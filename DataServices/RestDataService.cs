using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TodoMauiClient.Models;

namespace TodoMauiClient.DataServices
{
    public class RestDataService : IRestDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseAddress;
        private readonly string _url;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public RestDataService(HttpClient httpClient)
        {
            //_httpClient = new HttpClient();
            _httpClient = httpClient;
            _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5142" : "https://localhost:7032";
            _url = $"{_baseAddress}/api";
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task AddTodoAsync(Todo todo)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return;
            }

            try
            {
                string jsonTodo = JsonSerializer.Serialize<Todo>(todo, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonTodo, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/todo", content);
                if (response.IsSuccessStatusCode)
                {

                    Debug.WriteLine("Successfully created a todo");
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"---> Whoots execption; {ex.Message}");
            }
        }

        public async Task DeleteTodoAsync(int id)
        {

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return;
            }

            try
            {               
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/todo/{id}");
                if (response.IsSuccessStatusCode)
                {

                    Debug.WriteLine("Successfully deleted a todo");
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"---> Whoots execption; {ex.Message}");
            }
        }

        public async Task<List<Todo>> GetAllTodoAsync()
        {
            List<Todo> todos = new List<Todo>();

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return todos;
            }

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/todo");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    todos = JsonSerializer.Deserialize<List<Todo>>(content, _jsonSerializerOptions);
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"---> Whoots execption; {ex.Message}");
            }

            return todos;

        }

        public async Task UpdateTodoAsync(Todo todo)
        {

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                Debug.WriteLine("---> No internet access...");
                return;
            }

            try
            {
                string jsonTodo = JsonSerializer.Serialize<Todo>(todo, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonTodo, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/todo/{todo.Id}", content);
                if (response.IsSuccessStatusCode)
                {

                    Debug.WriteLine("Successfully updated a todo");
                }
                else
                {
                    Debug.WriteLine("---> Non Http 2xx response");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"---> Whoots execption; {ex.Message}");
            }
        }
    }
}
