using DotNetEnv;
using Microsoft.Extensions.Configuration;

namespace Bot1.App.Config
{
    public static class ENVLoader
    {
        public static IConfiguration LoadEnv()
        {
            //var projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName;
            var projectRoot = AppContext.BaseDirectory;
            var envPath = Path.Combine(projectRoot, ".env");
            var jsonPath = Path.Combine(projectRoot, "App", "appsettings.json");

            //if (!File.Exists(envPath))
           //     throw new Exception(".env file not found at " + envPath);

            //Env.Load(envPath);

            var _config = new ConfigurationBuilder()
                .SetBasePath(projectRoot)
                //.AddJsonFile("App/appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            var token = _config["DISCORDTOKEN"] ?? Environment.GetEnvironmentVariable("DISCORDTOKEN");
            if (string.IsNullOrWhiteSpace(token))
                throw new Exception("Discord token not found");

            return _config;
        }
    }
}
