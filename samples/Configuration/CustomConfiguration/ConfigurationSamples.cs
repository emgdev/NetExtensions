using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using EMG.Extensions.Configuration;
using Amazon;
using Amazon.Extensions.NETCore.Setup;

namespace NetExtensions
{
    public static class ConfigurationSamples
    {
        public static IConfigurationRoot ParameterStore()
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddSystemsManager("/emg", new AWSOptions { Region = RegionEndpoint.EUWest1 });

            var configuration = configurationBuilder.Build();

            return configuration;
        }

        public static IConfigurationRoot ECSContainerMetadata()
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddECSMetadataFile();

            var configuration = configurationBuilder.Build();

            return configuration;
        }

        public static IConfigurationRoot SecretsManager()
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddSecretsManager(region: RegionEndpoint.EUWest1);

            var configuration = configurationBuilder.Build();

            return configuration;
        }

        public static IConfigurationRoot Objects()
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddObject(new
            {
                Hello = "World",
                Foo = new
                {
                    Bar = "My name"
                },
                Ints = new[] { 1, 2 },
                Foos = new[]
                {
                    new { Bar = "Renato" },
                    new { Bar = "Golia" }
                }
            });

            var configuration = configurationBuilder.Build();

            return configuration;
        }
    }
}