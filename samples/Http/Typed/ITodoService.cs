using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo
{
    public class TodoItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public bool IsCompleted { get; set; }
    }

    public interface ITodoService
    {
        Task<IReadOnlyList<TodoItem>> GetAll();

        Task<TodoItem> Get(int id);
    }
}
