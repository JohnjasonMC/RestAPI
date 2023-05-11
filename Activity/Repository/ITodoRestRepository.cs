using Activity.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Activity.Repository
{
    public interface ITodoRestRepository
    {
        Task<List<Todo>> GetTodosAsync();
        Task<Todo> GetTodoAsync(int id);
        Task CreateTodoAsync(Todo todo);
        Task UpdateTodoAsync(Todo todo);
        Task DeleteTodoAsync(int id);
    }
}
