using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Todo;

namespace Typed
{
    public class HttpTodoService : ITodoService
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        private readonly HttpClient _http;

        public HttpTodoService(HttpClient http)
        {
            _http = http ?? throw new System.ArgumentNullException(nameof(http));
        }

        public async Task<TodoItem> Get(int id)
        {
            using var response = await _http.GetAsync($"/todos/{id}");

            using var stream = await response.Content.ReadAsStreamAsync();

            var item = await JsonSerializer.DeserializeAsync<TodoItem>(stream, JsonSerializerOptions);

            return item;
        }

        public async Task<IReadOnlyList<TodoItem>> GetAll()
        {
            using var response = await _http.GetAsync("/todos/");

            using var stream = await response.Content.ReadAsStreamAsync();

            var item = await JsonSerializer.DeserializeAsync<TodoItem[]>(stream, JsonSerializerOptions);

            return item;
        }

    }
}