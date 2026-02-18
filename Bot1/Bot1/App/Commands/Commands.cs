using Discord.Interactions;
using Bot1.Domain.Interfaces;
using AutoMapper;
using Discord;
using Bot1.App.Config.Settings;

namespace Bot1.App.Commands
{
    public class Commands : InteractionModuleBase<SocketInteractionContext>
    {
        private IServerService _serverService;
        private IMapper _mapper;
        private ulong _king;

        public Commands(IServerService serverService, IMapper mapper, DiscordSettings settings)
        {
            _serverService = serverService;
            _king = settings.KingRoleId;
            _mapper = mapper;
        }
        
        [SlashCommand("hello", "Says hello world", runMode: RunMode.Async)]
        public async Task Hello()
        {
            await DeferAsync();
            try
            {
                Console.WriteLine($"User {Context.User.Id} uses hello. Current king: {_king}. Access: {Context.User.Id == _king}");
                if (Context.User.Id != _king)
                    await FollowupAsync("Hello!");
                else
                {
                    string servers = await _serverService.GetAllServers();
                    if (string.IsNullOrEmpty(servers))
                        await FollowupAsync(servers, ephemeral: true);
                    else
                        await FollowupAsync($"Towns: \n {servers}", ephemeral: true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await FollowupAsync("Hello!", ephemeral: true);
            }
        }

        [SlashCommand("help", "How to use and list of commands")]
        public async Task Help()
        {
            await DeferAsync();
            var embed = new EmbedBuilder()
            .WithTitle("Town Manager Bot - Help Menu")
            .WithDescription("This bot allows you to create, manage, and track town expirations seamlessly.")
            .WithColor(Color.Blue)
            .AddField("Town Management",
                "`/town add` - Register a new server (Requires Role).\n" +
                "`/town delete` - Removes an existing server (Requires Role).\n" +
                "`/town update` - Modify settings or extend server time (Requires Role).\n" +
                "`/town list` - See all servers you have registered.")
            .AddField("Pro Tip", "When creating a server, you can specify hours. If left blank, it defaults to **24 hours**.")
            .WithCurrentTimestamp();

            await FollowupAsync(embed: embed.Build(), ephemeral: true);
        }
    }
}
