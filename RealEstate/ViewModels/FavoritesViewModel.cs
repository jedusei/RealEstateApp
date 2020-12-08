using Acr.UserDialogs;
using MvvmHelpers.Commands;
using RealEstate.Models;
using RealEstate.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using RealEstate.Views;

namespace RealEstate.ViewModels
{
    public class FavoritesViewModel : BaseViewModel
    {
        IRentalService _rentalService;

        public ObservableCollection<Rental> Rentals { get; private set; }
        public ICommand RemoveItemCommand { get; private set; }
        public AsyncCommand<Rental> OpenRentalDetailsCommand { get; private set; }

        public FavoritesViewModel()
        {
            _rentalService = DependencyService.Get<IRentalService>();
            _loadStatus = LoadStatus.Loading;
            RemoveItemCommand = new AsyncCommand<Rental>(async (rental) =>
            {
                bool proceed = await UserDialogs.Instance.ConfirmAsync("Are you sure you want to remove this item from your favorites list?", "Remove Item");
                if (proceed)
                    rental.IsFavorite = false;
            });
            OpenRentalDetailsCommand = new AsyncCommand<Rental>(async (rental) =>
            {
                await _navigationService.GoToPageAsync<RentalDetailsPage>(new RentalDetailsPage.Args
                {
                    Rental = rental
                });
            });
        }

        public override async void OnStart()
        {
            base.OnStart();
            Rentals = await _rentalService.GetFavoriteRentalsAsync();
            OnPropertyChanged(nameof(Rentals));
            LoadStatus = LoadStatus.Loaded;
        }
    }
}
