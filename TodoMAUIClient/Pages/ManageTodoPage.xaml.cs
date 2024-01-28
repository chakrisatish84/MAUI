using System.Diagnostics;
using TodoMauiClient.DataServices;
using TodoMauiClient.Models;

namespace TodoMauiClient.Pages;

[QueryProperty(nameof(Todo), "Todo")]
public partial class ManageTodoPage : ContentPage
{
    private readonly IRestDataService _dataService;
    public Todo? _todo;
    public bool _isNew;

    public Todo Todo
    {
        get => _todo ?? new Todo();
        set
        {
            _todo = value;
            _isNew = IsNew(value);
            OnPropertyChanged();
        }
    }

    public ManageTodoPage(IRestDataService dataService)
    {
        InitializeComponent();

        _dataService = dataService;
        BindingContext = this;
    }

    private bool IsNew(Todo todo)
    {
        if (todo.Id == 0)
        {
            return true;
        }
        return false;
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        if (_isNew)
        {
            Debug.WriteLine("----> Add new Item");
            await _dataService.AddTodoAsync(Todo);
        }
        else
        {

            Debug.WriteLine("---> Update a Todo item");
            await _dataService.UpdateTodoAsync(Todo);
        }
        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        bool result = await DisplayAlert("Confirmation", "Are sure to delete this todo item?", "Yes", "No");

        if (result)
        {
            await _dataService.DeleteTodoAsync(Todo.Id);           
        }
        await Shell.Current.GoToAsync("..");
    }

    async void onCancelButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}