using RealEstate.Models;
using RealEstate.Services;
using Xamarin.Forms;

namespace RealEstate.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        IRentalService _rentalService;

        public Rental[] Rentals { get; private set; }
        public Command<Rental> ToggleFavoriteCommand { get; private set; }

        public HomeViewModel()
        {
            _rentalService = DependencyService.Get<IRentalService>();
            _loadStatus = LoadStatus.Loading;
            ToggleFavoriteCommand = new Command<Rental>(rental => rental.IsFavorite = !rental.IsFavorite);
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
