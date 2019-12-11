using System;
using Microsoft.Extensions.Configuration;

namespace UsingConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = CreateConfiguration();

            // Access by key/value
            Console.WriteLine($"FooBar={configuration["Foo:Bar"]}");

            // Access by section
            Console.WriteLine($"FooBar={configuration.GetSection("Foo")["Bar"]}");

            // Raw binding
            var config = new MyConfig();
            configuration.Bind(config);
            Console.WriteLine($"Hello={config.Hello}");

            // Bind to simple property
            Console.WriteLine($"Hello={configuration.GetSection("Hello").Get<string>()}");

            // Bind to array
            var ints = configuration.GetSection("Ints").Get<int[]>();

            // Bind to complex object
            Console.WriteLine($"Hello={configuration.Get<MyConfig>().Hello}");
        }

        private static IConfigurationRoot CreateConfiguration()
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

    public class MyConfig
    {
        public string Hello { get; set; }
    }
}
