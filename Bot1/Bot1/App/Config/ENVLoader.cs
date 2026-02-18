using DotNetEnv;
using Microsoft.Extensions.Configuration;

namespace Bot1.App.Config
{
    public static class ENVLoader
    {
        public static IConfiguration LoadEnv()
        {
            //var projectRoot = Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName; //local
            var projectRoot = AppContext.BaseDirectory; //remote
            var envPath = Path.Combine(projectRoot, ".env");
            var jsonPath = Path.Combine(projectRoot, "App", "appsettings.json");

            //if (!File.Exists(envPath))
            //    throw new Exception(".env file not found at " + envPath);

            //Env.Load(envPath);

            var _config = new ConfigurationBuilder()
                .SetBasePath(projectRoot)
                .AddEnvironmentVariables()
                .Build();

            return _config;
        }
    }
}
