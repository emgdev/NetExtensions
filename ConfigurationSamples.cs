using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace NetExtensions
{
    public static class ConfigurationSamples
    {
        public static IConfigurationRoot BuilderOnly()
        {
            var configurationBuilder = new ConfigurationBuilder();

            // ... add providers

            var configuration = configurationBuilder.Build();

            return configuration;
        }

        public static IConfigurationRoot InMemory()
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                ["Hello"] = "World",
                ["Foo:Bar"] = "My name",
                ["Ints:0"] = "1",
                ["Ints:1"] = "2",
                ["Foos:0:Bar"] = "Renato",
                ["Foos:1:Bar"] = "Golia"
            });

            var configuration = configurationBuilder.Build();

            return configuration;
        }

        public static IConfigurationRoot FileIni()
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);

            configurationBuilder.AddIniFile("appsettings.ini", false, true);

            var configuration = configurationBuilder.Build();

            return configuration;
        }

        public static IConfigurationRoot FileJson()
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);

            configurationBuilder.AddJsonFile("appsettings.json", false, true);

            var configuration = configurationBuilder.Build();

            return configuration;
        }

        public static IConfigurationRoot FileXml()
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddXmlFile("appsettings.xml", false, true);

            var configuration = configurationBuilder.Build();

            return configuration;
        }

        public static IConfigurationRoot KeyPerFile()
        {
            var configurationBuilder = new ConfigurationBuilder();

            // ... add providers

            var configuration = configurationBuilder.Build();

            return configuration;
        }

        public static IConfigurationRoot EnvironmentVariables()
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();

            return configuration;
        }

        public static IConfigurationRoot CommandLineArguments(string[] args)
        {
            // not reachable from Program due to incompatibility with McMaster.Extensions.CommandLineUtils
            // dotnet run -- -h World --foo 'My Name'

            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddCommandLine(args, new Dictionary<string, string>
            {
                ["-h"] = "Hello",
                ["-fb"] = "Foo:Bar",
                ["--hello"] = "Hello",
                ["--foo"] = "Foo:Bar"
            });

            var configuration = configurationBuilder.Build();

            return configuration;
        }
    }
}
