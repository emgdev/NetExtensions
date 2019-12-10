using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace NetExtensions
{
    public class ConfigurationSamples
    {
        public void Builder()
        {
            var configurationBuilder = new ConfigurationBuilder();

            // ... add providers

            var configuration = configurationBuilder.Build();
        }

        public void InMemory()
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
        }

        public void CommandLineArguments(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddCommandLine(args, new Dictionary<string, string>
            {
                ["-h"] = "Hello",
                ["-fb"] = "Foo:Bar",
                ["--hello"] = "Hello",
                ["--foo"] = "Foo:Bar"
            });

            var configuration = configurationBuilder.Build();

        }
    }
}
