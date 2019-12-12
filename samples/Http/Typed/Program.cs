using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Todo;

namespace Typed
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddHttpClient<ITodoService, HttpTodoService>().ConfigureHttpClient(http =>
            {
                http.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            });

            var serviceProvider = services.BuildServiceProvider();

            var todoService = serviceProvider.GetRequiredService<ITodoService>();

            var todos = await todoService.GetAll();

            foreach (var todo in todos)
            {
                Console.WriteLine($"{todo.Id}: {todo.Title}");
            }
        }
    }
}
