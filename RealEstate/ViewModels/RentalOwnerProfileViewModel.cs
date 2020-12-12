using RealEstate.Models;
using RealEstate.Services;
using System.Windows.Input;
using Xamarin.Forms;
using RealEstate.Views;
using MvvmHelpers.Commands;
using Xamarin.Essentials;
using System.Threading.Tasks;
using MvvmHelpers;

namespace RealEstate.ViewModels
{
    public class RentalOwnerProfileViewModel : BaseViewModel
    {
        IRentalService _rentalService;

        public RentalOwner Owner { get; private set; }
        public ObservableRangeCollection<Rental> Rentals { get; private set; } = new ObservableRangeCollection<Rental>();
        public ICommand SendEmailCommand { get; private set; }
        public ICommand CallCommand { get; private set; }
        public ICommand ToggleFavoriteCommand { get; private set; }
        public AsyncCommand<Rental> OpenRentalDetailsCommand { get; private set; }


        public RentalOwnerProfileViewModel()
        {
            _rentalService = DependencyService.Get<IRentalService>();
            ToggleFavoriteCommand = new Xamarin.Forms.Command<Rental>(rental => rental.IsFavorite = !rental.IsFavorite);
            SendEmailCommand = new AsyncCommand(() => Launcher.TryOpenAsync($"mailto:{Owner.Email}"));
            CallCommand = new Xamarin.Forms.Command(() =>
            {
                try
                {
                    PhoneDialer.Open(Owner.PhoneNumber);
                }
                catch { }
            });
            OpenRentalDetailsCommand = new AsyncCommand<Rental>(async (rental) =>
            {
                await _navigationService.GoToPageAsync<RentalDetailsPage>(new RentalDetailsPage.Args
                {
                    Rental = rental
                });
            });
        }

        public override void Initialize(object navigationData)
        {
            var args = navigationData as RentalOwnerProfilePage.Args;
            Owner = args.Owner;
            OnPropertyChanged(nameof(Owner));
        }

        public override async void OnStart()
        {
            base.OnStart();
            await LoadRentalsAsync();
        }

        public override async void OnResume(object navigationData = null)
        {
            base.OnResume(navigationData);
            if (navigationData != null)
            {
                Initialize(navigationData);
                await LoadRentalsAsync();
            }
        }

        async Task LoadRentalsAsync()
        {
            LoadStatus = LoadStatus.Loading;
            Rentals.Clear();
            Rentals.AddRange(await _rentalService.GetRentalsAsync(Owner));
            LoadStatus = LoadStatus.Loaded;
        }
    }
}
