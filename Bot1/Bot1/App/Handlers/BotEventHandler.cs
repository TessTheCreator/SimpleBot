using Discord.Interactions;
using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using Bot1.App.Config.Settings;

namespace Bot1.App.Handlers
{
    public class BotEventHandler
    {
        private readonly InteractionService _interactions;
        private readonly IServiceProvider _services;
        private readonly DiscordSocketClient _client;

        private ulong _guildId;

        public BotEventHandler(
            InteractionService interactions,
            IServiceProvider services,
            DiscordSettings settings,
            DiscordSocketClient client)
        {
            _interactions = interactions;
            _services = services;
            _client = client;
            _guildId = settings.GuildId;
        }

        public async Task Ready()
        {
            Console.WriteLine($"INTERNAL CHECK: Modules: {_interactions.Modules.Count} | Commands: {_interactions.SlashCommands.Count}");
            
            if (_guildId == null)
                throw new Exception("Guild Id does not exist");
            
            _ = Task.Run(async () => {
                await _interactions.RegisterCommandsToGuildAsync(_guildId, true);
            });
        }

        public async Task HandleInteraction(SocketInteraction interaction)
        {
            Console.WriteLine($"Interaction start: {interaction.Type}");

            using (var scope = _services.CreateScope())
            {
                try
                {
                    var ctx = new SocketInteractionContext(_client, interaction);
                    var result = await _interactions.ExecuteCommandAsync(ctx, scope.ServiceProvider);

                    if (result.IsSuccess)
                        Console.WriteLine("Command executed successfully!");
                    else
                        Console.WriteLine($"Result: {result.Error} | {result.ErrorReason}");
                }
                catch (Exception ex) { Console.WriteLine($"EX: {ex}"); }
            }
        }

        public Task Log(LogMessage msg)
        {
            Console.WriteLine(msg);
            return Task.CompletedTask;
        }
    }

}
