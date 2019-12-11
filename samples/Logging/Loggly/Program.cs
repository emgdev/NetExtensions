using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace Loggly
{
    class Program
    {
        static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddObject(new
            {
                Logging = new
                {
                    LogLevel = new
                    {
                        Default = "Trace"
                    }
                },
                Loggly = new
                {
                    ApplicationName = "My application name",
                    ApiKey = "loggly-api-key"
                }
            });

            var configuration = configurationBuilder.Build();

            var services = new ServiceCollection();

            services.AddLogging(logging =>
            {
                logging.AddConfiguration(configuration.GetSection("Logging"));

                logging.AddLoggly(configuration.GetSection("Loggly"), loggly =>
                {
                    // customize the provider
                    loggly.SuppressExceptions = false;
                });
            });

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogTrace("This is a trace message");
            logger.LogDebug("This is a debug message");
            logger.LogInformation("This is a information message");
            logger.LogWarning("This is a warning message");
            logger.LogError("This is a error message");
            logger.LogCritical("This is a critical message");

            Console.WriteLine("Done!");

            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
        }

    }
}

