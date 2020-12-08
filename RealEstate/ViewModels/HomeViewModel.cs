using MvvmHelpers.Commands;
using RealEstate.Models;
using RealEstate.Services;
using RealEstate.Views;

namespace RealEstate.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        IRentalService _rentalService;

        public Rental[] Rentals { get; private set; }
        public Command<Rental> ToggleFavoriteCommand { get; private set; }
        public AsyncCommand<Rental> OpenRentalDetailsCommand { get; private set; }

        public HomeViewModel()
        {
            _rentalService = Xamarin.Forms.DependencyService.Get<IRentalService>();
            _loadStatus = LoadStatus.Loading;
            ToggleFavoriteCommand = new Command<Rental>(rental => rental.IsFavorite = !rental.IsFavorite);
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
            Rentals = await _rentalService.GetRentalsAsync();
            OnPropertyChanged(nameof(Rentals));
            LoadStatus = LoadStatus.Loaded;
        }
    }
}
