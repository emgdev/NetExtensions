using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AWSDependencyInjection
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
                }
            });

            var configuration = configurationBuilder.Build();

            var services = new ServiceCollection();

            services.AddDefaultAWSOptions(configuration.GetAWSOptions());

            services.AddAWSService<IAmazonS3>();

            services.AddSingleton(new S3Options
            {
                BucketName = "emg-tech-temp",
                Prefix = "test"
            });

            services.AddTransient<IMessenger, S3Messenger>();

            var serviceProvider = services.BuildServiceProvider();

            var messenger = serviceProvider.GetRequiredService<IMessenger>();

            await messenger.WriteMessage("Hello world");

            Console.WriteLine("Done");
        }
    }
}
