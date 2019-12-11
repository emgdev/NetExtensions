using System;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;

namespace StandardDependencyInjection
{
    class Program
    {
        static Task<int> Main(string[] args) => CommandLineApplication.ExecuteAsync<Program>(args);

        [Argument(0, "Demo", "Select the demo to run")]
        public Demo DemoToRun { get; set; } = Demo.Service;

        private Task OnExecuteAsync(CommandLineApplication app)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<ConsoleMessengerOptions>();

            services = DemoToRun switch
            {
                Demo.Service => ServiceRegistration(services),

                Demo.Implementation => ImplementationRegistration(services),

                Demo.ServiceInstance => ServiceInstanceRegistration(services),

                Demo.ImplementationInstance => ImplementationInstanceRegistration(services),

                Demo.FactoryMethodService => FactoryMethodServiceRegistration(services),

                Demo.FactoryMethodImplementation => FactoryMethodImplementationRegistration(services),

                _ => throw new NotSupportedException()
            };

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var messenger = scope.ServiceProvider.GetRequiredService<IMessenger>();

                messenger.WriteMessage("Hello World");
            }

            return Task.CompletedTask;
        }

        static IServiceCollection ServiceRegistration(IServiceCollection services)
        {
            // registers a service for an abstraction
            return services.AddSingleton<IMessenger, ConsoleMessenger>();
        }

        static IServiceCollection ImplementationRegistration(IServiceCollection services)
        {
            // registers a service for itself
            // this will not match "IMessenger"s requests
            return services.AddSingleton<ConsoleMessenger>();
        }

        static IServiceCollection ServiceInstanceRegistration(IServiceCollection services)
        {
            // registers a specific instance
            var instance = new ConsoleMessenger();

            return services.AddSingleton<IMessenger>(instance);
        }

        static IServiceCollection ImplementationInstanceRegistration(IServiceCollection services)
        {
            // registers a specific instance
            var instance = new ConsoleMessenger();

            return services.AddSingleton(instance);
        }

        static IServiceCollection FactoryMethodServiceRegistration(IServiceCollection services)
        {
            // registers a service by providing a factory method
            return services.AddSingleton<IMessenger>((IServiceProvider sp) =>
            {
                var options = sp.GetRequiredService<ConsoleMessengerOptions>();

                return new ConsoleMessenger(options);
            });
        }

        static IServiceCollection FactoryMethodImplementationRegistration(IServiceCollection services)
        {
            // registers an implementation by providing a factory method
            return services.AddSingleton<ConsoleMessenger>((IServiceProvider sp) =>
            {
                var options = sp.GetRequiredService<ConsoleMessengerOptions>();

                return new ConsoleMessenger(options);
            });
        }
    }

    public enum Demo
    {
        Service,
        Implementation,
        ServiceInstance,
        ImplementationInstance,
        FactoryMethodService,
        FactoryMethodImplementation
    }
}
