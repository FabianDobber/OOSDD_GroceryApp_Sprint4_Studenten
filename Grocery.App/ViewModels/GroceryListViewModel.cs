using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;

namespace Grocery.App.ViewModels
{
    public partial class GroceryListViewModel : BaseViewModel
    {
        public ObservableCollection<GroceryList> GroceryLists { get; } = new();

        private readonly IGroceryListService _groceryListService;
        private readonly IAuthService _authenticationService;

        [ObservableProperty]
        private Client client;

        public GroceryListViewModel(IGroceryListService groceryListService, IAuthService authenticationService)
        {
            Title = "Boodschappenlijst";
            _groceryListService = groceryListService;
            _authenticationService = authenticationService;

            LoadGroceryLists();
            Client = _authenticationService.CurrentClient;
        }
        [RelayCommand]
        public async Task ShowBoughtProducts()
        {
            if (Client?.Role == Role.Admin)
            {
                await Shell.Current.GoToAsync(nameof(Views.BoughtProductsView));
            }
        }
        [RelayCommand]
        public async Task SelectGroceryList(GroceryList groceryList)
        {
            var parameter = new Dictionary<string, object> { { nameof(GroceryList), groceryList } };
            await Shell.Current.GoToAsync(
                $"{nameof(Views.GroceryListItemsView)}?Titel={groceryList.Name}",
                true,
                parameter
            );
        }
        public override void OnAppearing()
        {
            base.OnAppearing();
            LoadGroceryLists();
            Client = _authenticationService.CurrentClient;

        }
        public override void OnDisappearing()
        {
            base.OnDisappearing();
            GroceryLists.Clear();
        }
        private void LoadGroceryLists()
        {
            GroceryLists.Clear();
            foreach (var list in _groceryListService.GetAll())
            {
                GroceryLists.Add(list);
            }
        }
    }
}
