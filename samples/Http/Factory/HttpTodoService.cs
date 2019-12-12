using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Todo;

namespace Factory
{
    public class HttpTodoService : ITodoService
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        private readonly IHttpClientFactory _httpFactory;

        public HttpTodoService(IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory ?? throw new System.ArgumentNullException(nameof(httpFactory));
        }

        private HttpClient Http() => _httpFactory.CreateClient(nameof(HttpTodoService));

        public async Task<TodoItem> Get(int id)
        {
            using var response = await Http().GetAsync($"/todos/{id}");

            using var stream = await response.Content.ReadAsStreamAsync();

            var item = await JsonSerializer.DeserializeAsync<TodoItem>(stream, JsonSerializerOptions);

            return item;
        }

        public async Task<IReadOnlyList<TodoItem>> GetAll()
        {
            using var response = await Http().GetAsync("/todos/");

            using var stream = await response.Content.ReadAsStreamAsync();

            var item = await JsonSerializer.DeserializeAsync<TodoItem[]>(stream, JsonSerializerOptions);

            return item;
        }
    }
}