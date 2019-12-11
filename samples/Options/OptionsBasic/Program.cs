using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OptionsBasic
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddObject(new
            {
                AWS = new
                {
                    Region = RegionEndpoint.EUWest1.SystemName
                },
                S3 = new
                {
                    BucketName = "emg-tech-temp",
                    Prefix = "test"
                }
            });

            var configuration = configurationBuilder.Build();

            var services = new ServiceCollection();

            services.AddOptions();

            services.Configure<S3Options>(configuration.GetSection("S3"));

            services.AddSingleton<IMessenger, S3Messenger>();

            services.AddDefaultAWSOptions(configuration.GetAWSOptions());

            services.AddAWSService<IAmazonS3>();

            var serviceProvider = services.BuildServiceProvider();

            var messenger = serviceProvider.GetRequiredService<IMessenger>();

            await messenger.WriteMessage("Hello world");

            Console.WriteLine("Done");

        }
    }

}
