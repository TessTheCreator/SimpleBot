using Bot1.Data.Interfaces;
using Bot1.Data.Repositories;
using Bot1.Domain.Interfaces;
using Bot1.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Discord.Interactions;
using Discord.WebSocket;
using Discord;
using Bot1.App.Handlers;
using Bot1.Data;
using Bot1.App.Config.Settings;

namespace Bot1.App.Config
{
    public static class Configuration
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IServerRepository, ServerRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSingleton<IServerService, ServerService>();
            services.AddScoped<IEventService, EventService>();
            services.AddSingleton<BotEventHandler>();
            services.AddSingleton<ExpirationWorker>();
            return services;
        }

        public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration config)
        {
            var section = config.GetSection("DiscordSettings");

            var discordSettings = new DiscordSettings();
            section.Bind(discordSettings);

            services.AddSingleton(discordSettings);

            return services;
        }

        public static IServiceCollection ConfigureDiscordBot(this IServiceCollection services)
        {
            services.AddSingleton<DiscordSocketClient>(sp =>
                new DiscordSocketClient(new DiscordSocketConfig
                {
                    GatewayIntents = GatewayIntents.All
                })
            );

            services.AddSingleton<InteractionService>(sp =>
            {
                var client = sp.GetRequiredService<DiscordSocketClient>();
                return new InteractionService(client, new InteractionServiceConfig
                {
                    DefaultRunMode = RunMode.Async
                });
            });

            return services;
        }

        public static IServiceCollection ConfigureDB(this IServiceCollection services, IConfiguration config)
        {
            //var rootPath = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName; //local
            var rootPath = AppContext.BaseDirectory; //hosting
            var dbFolder = Path.Combine(rootPath, "Data", "DB");
            var dbPath = Path.Combine(dbFolder, "req.db");
            services.AddDbContext<BotDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));

            return services;
        }
    }
}
