using Activity.Models;
using Newtonsoft.Json;
using System.Text;

namespace Activity.Repository
{
    public class TodoRestRepository : ITodoRestRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public TodoRestRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "https://jsonplaceholder.typicode.com/todos";
        }
        
        public async Task<List<Todo>> GetTodosAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Todo>>(jsonString);
        }

        public async Task<Todo> GetTodoAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Todo>(jsonString);
        }

        public async Task CreateTodoAsync(Todo todo)
        {
            var json = JsonConvert.SerializeObject(todo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateTodoAsync(Todo todo)
        {
            var json = JsonConvert.SerializeObject(todo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/{todo.Id}", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTodoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
