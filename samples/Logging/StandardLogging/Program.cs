using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace StandardLogging
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
                    },
                    Console = new
                    {
                        LogLevel = new Dictionary<string, string>
                        {
                            ["Microsoft.Extensions"] = "Error"
                        }
                    }
                }
            });

            var configuration = configurationBuilder.Build();

            configuration.AsEnumerable().ToList().ForEach(o => Console.WriteLine($"{o.Key}={o.Value}"));

            var services = new ServiceCollection();

            services.AddLogging(logging =>
            {
                logging.AddConfiguration(configuration.GetSection("Logging"));

                logging.AddConsole();
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

            Console.ReadLine();
        }
    }
}
