using System;
using Microsoft.Extensions.Configuration;
using EMG.Extensions.Configuration;
using System.Threading.Tasks;

namespace MultipleConfiguration
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var hostConfiguration = ConfigureHost();

            var configuration = ConfigureApplication(hostConfiguration, args);

            foreach (var item in configuration.AsEnumerable())
            {
                await Console.Out.WriteLineAsync($"{item.Key}='{item.Value}'");
            }
        }

        private static IConfiguration ConfigureHost()
        {
            var hostConfigurationBuilder = new ConfigurationBuilder();

            hostConfigurationBuilder.AddObject(new
            {
                ApplicationName = "EMG Tech Academy Test Project",
                Environment = "Development"
            });

            hostConfigurationBuilder.AddEnvironmentVariables("DOTNET_");
            hostConfigurationBuilder.AddECSMetadataFile();

            var hostConfiguration = hostConfigurationBuilder.Build();

            return hostConfiguration;
        }

        private static IConfiguration ConfigureApplication(IConfiguration hostConfiguration, string[] arguments)
        {
            var environment = hostConfiguration["Environment"];

            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddConfiguration(hostConfiguration);

            if (environment is "Development")
            {
                configurationBuilder.AddUserSecrets<Program>();
            }

            configurationBuilder.AddJsonFile("appsettings.json", false);
            configurationBuilder.AddJsonFile($"appsettings.{environment}.json", true);

            configurationBuilder.AddEnvironmentVariables("EMG_");

            configurationBuilder.AddCommandLine(arguments);

            var configuration = configurationBuilder.Build();

            return configuration;
        }
    }
}
