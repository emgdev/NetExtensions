using System;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Configuration;

namespace NetExtensions
{
    class Program
    {
        // dotnet run -- <Demo>
        // e.g. dotnet run -- FileJson
        static Task<int> Main(string[] args) => CommandLineApplication.ExecuteAsync<Program>(args);

        [Argument(0, "Demo", "Select the demo to run")]
        public Demo DemoToRun { get; set; } = Demo.BuilderOnly;

        private async Task OnExecuteAsync(CommandLineApplication app)
        {
            IConfigurationRoot configuration = DemoToRun switch
            {
                Demo.BuilderOnly => ConfigurationSamples.BuilderOnly(),

                Demo.InMemory => ConfigurationSamples.InMemory(),

                Demo.EnvironmentVariables => ConfigurationSamples.EnvironmentVariables(),

                Demo.FileXml => ConfigurationSamples.FileXml(),

                Demo.FileJson => ConfigurationSamples.FileJson(),

                Demo.FileIni => ConfigurationSamples.FileIni(),

                Demo.UserSecrets => ConfigurationSamples.UserSecrets(),

                _ => throw new NotSupportedException()
            };

            await app.Out.WriteLineAsync($"Printing all configuration received from {DemoToRun}");

            foreach (var item in configuration.AsEnumerable())
            {
                await app.Out.WriteLineAsync($"{item.Key}='{item.Value}'");
            }
        }
    }

    public enum Demo
    {
        BuilderOnly,
        InMemory,
        EnvironmentVariables,
        FileXml,
        FileJson,
        FileIni,
        UserSecrets
    }
}
