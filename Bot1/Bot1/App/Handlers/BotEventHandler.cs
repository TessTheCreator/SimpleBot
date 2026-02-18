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

            if (_guildId == 0) throw new Exception("Guild Id is missing from config!");

            try
            {
                await _interactions.RegisterCommandsToGuildAsync(_guildId, true);

                Console.WriteLine($"SUCCESS: Registered {_interactions.SlashCommands.Count} commands to Guild {_guildId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"REGISTRATION ERROR: {ex.Message}");
            }
        }

        public async Task HandleInteraction(SocketInteraction interaction)
        {
            Console.WriteLine($"Context User: {interaction.User.Username} | Guild: {interaction.GuildId}");
            Console.WriteLine($"Interaction start: {interaction.Type}");

            using (var scope = _services.CreateScope())
            {
                try
                {
                    var ctx = new SocketInteractionContext(_client, interaction);
                    Console.WriteLine($"Attempting to execute: {interaction.Data.ToString()}");
                    var result = await _interactions.ExecuteCommandAsync(ctx, scope.ServiceProvider);
                    Console.WriteLine($"Result: {result.Error} - {result.ErrorReason}");

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
