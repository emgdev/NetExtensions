using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Todo;

namespace Factory
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddHttpClient(nameof(HttpTodoService)).ConfigureHttpClient(http =>
            {
                http.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
            });

            services.AddSingleton<ITodoService, HttpTodoService>();

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
