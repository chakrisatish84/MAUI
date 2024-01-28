using System.Diagnostics;
using TodoMauiClient.DataServices;
using TodoMauiClient.Models;
using TodoMauiClient.Pages;

namespace TodoMauiClient
{
    public partial class MainPage : ContentPage
    {
        private readonly IRestDataService _dataService;

        public MainPage(IRestDataService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            collectionView.ItemsSource = await _dataService.GetAllTodoAsync();
        }

         async void OnAddTodoClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("---> Add button clicked");
            var navigationParameter = new Dictionary<string, object> 
            {
                {nameof(Todo), new Todo() }
            };

            await Shell.Current.GoToAsync(nameof(ManageTodoPage), navigationParameter);
        }

        async void onSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("---> Selection changed clicked!");
            var navigationParameter = new Dictionary<string, object>
            {
                {nameof(Todo), e.CurrentSelection.FirstOrDefault() as Todo }
            };

            await Shell.Current.GoToAsync(nameof(ManageTodoPage), navigationParameter);
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }
    }

}
