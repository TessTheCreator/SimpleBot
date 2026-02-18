using Discord;
using Discord.WebSocket;
using Discord.Interactions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Bot1.App.Config;
using Bot1.App.Handlers;
using Bot1.Data;
using System.Net;
using Bot1.App.Config.Settings;

namespace Bot1
{
    public class Program
    {
        private DiscordSocketClient _client;
        private InteractionService _interactions;
        private IServiceProvider _services;
        private IConfiguration _config;
        private BotEventHandler _handler;

    public static Task Main(string[] args) => new Program().MainAsync();
        public async Task MainAsync()
        {
            _config = ENVLoader.LoadEnv();

            var services = new ServiceCollection();
            services
                .ConfigureSettings(_config)
                .ConfigureDiscordBot()
                .ConfigureDB(_config)
                .ConfigureRepositories()
                .ConfigureServices();
            _services = services.BuildServiceProvider();

            var devSettings = _services.GetRequiredService<DevSettings>();
            var discordSettings = _services.GetRequiredService<DiscordSettings>();

            var token = discordSettings.DiscordBotToken;
            if (token == null)
                Console.WriteLine("Token is null");

            if (devSettings.IsLocal != true)
            {
                var listener = new HttpListener();
                listener.Prefixes.Add("http://*:8080/");
                listener.Start();
                _ = Task.Run(() => {
                    while (true)
                    {
                        var context = listener.GetContext();
                        context.Response.StatusCode = 200;
                        context.Response.Close();
                    }
                });
                Console.WriteLine("Fake Web Server running on Port 8080...");
            }

            using (var scope = _services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BotDbContext>();
                dbContext.Database.EnsureCreated();
            }

            var worker = _services.GetRequiredService<ExpirationWorker>();
            _ = worker.StartAsync(new CancellationToken());

            _client = _services.GetRequiredService<DiscordSocketClient>();
            _interactions = _services.GetRequiredService<InteractionService>();
            _handler = _services.GetRequiredService<BotEventHandler>();

            _client.Log += _handler.Log;
            _client.Ready += async () => await _handler.Ready();
            _client.InteractionCreated += async (interaction) => await _handler.HandleInteraction(interaction);

            await _interactions.AddModulesAsync(System.Reflection.Assembly.GetEntryAssembly(), _services);


            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
    }
}
