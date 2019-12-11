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
        public Demo DemoToRun { get; set; } = Demo.Objects;

        private async Task OnExecuteAsync(CommandLineApplication app)
        {
            IConfigurationRoot configuration = DemoToRun switch
            {
                Demo.ParameterStore => ConfigurationSamples.ParameterStore(),

                Demo.ECSContainerMetadata => ConfigurationSamples.ECSContainerMetadata(),

                Demo.SecretsManager => ConfigurationSamples.SecretsManager(),

                Demo.Objects => ConfigurationSamples.Objects(),

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
        ParameterStore,
        ECSContainerMetadata,
        SecretsManager,
        Objects
    }
}
