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

            configurationBuilder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);

            configurationBuilder.AddXmlFile("appsettings.xml", false, true);

            var configuration = configurationBuilder.Build();

            return configuration;
        }

        public static IConfigurationRoot EnvironmentVariables()
        {
            /*
            EMG_Hello=World
            EMG_Foo__Bar=My Name
            EMG_Ints__0=1
            EMG_Ints__1=2
            EMG_Foos__0__Bar=Renato
            EMG_Foos__1__Bar=Golia
            */

            var configurationBuilder = new ConfigurationBuilder();

            // includes all envs available to the process
            //configurationBuilder.AddEnvironmentVariables();

            // filters out all envs not starting with "EMG_"
            configurationBuilder.AddEnvironmentVariables("EMG_");

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

        public static IConfigurationRoot UserSecrets()
        {
            // to initialise your local store with values
            // type .\appsettings.json | dotnet user-secrets set

            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddUserSecrets<Program>();

            var configuration = configurationBuilder.Build();

            return configuration;
        }
    }
}