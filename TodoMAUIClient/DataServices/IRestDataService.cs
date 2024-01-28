using TodoMauiClient.Models;

namespace TodoMauiClient.DataServices
{
    public interface IRestDataService
    {
       public Task<List<Todo>> GetAllTodoAsync();
        Task AddTodoAsync(Todo todo);
        Task DeleteTodoAsync(int id);
        Task UpdateTodoAsync(Todo todo);
    }
}
