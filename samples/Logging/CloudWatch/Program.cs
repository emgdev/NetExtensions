using System;
using Amazon;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace CloudWatch
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
                }
            });

            var configuration = configurationBuilder.Build();

            var services = new ServiceCollection();

            services.AddLogging(logging =>
            {
                logging.AddConfiguration(configuration.GetSection("Logging"));

                logging.AddAWSProvider(new AWS.Logger.AWSLoggerConfig
                {
                    Region = RegionEndpoint.EUWest1.SystemName,
                    LogGroup = "/test/log"
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

