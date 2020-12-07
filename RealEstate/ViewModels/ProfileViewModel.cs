using RealEstate.Models;
using RealEstate.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace RealEstate.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        IUserService _userService;
        IRentalService _rentalService;

        public User User => _userService.User;
        public ObservableCollection<Rental> RentalHistory { get; private set; }
        public ICommand RemoveItemCommand { get; private set; }


        public ProfileViewModel()
        {
            _userService = DependencyService.Get<IUserService>();
            _rentalService = DependencyService.Get<IRentalService>();
            _loadStatus = LoadStatus.Loading;
            RemoveItemCommand = new Command<Rental>(rental => rental.IsFavorite = !rental.IsFavorite);
        }

        public override async void OnStart()
        {
            base.OnStart();
            RentalHistory = await _rentalService.GetRentalHistoryAsync();
            OnPropertyChanged(nameof(RentalHistory));
            LoadStatus = LoadStatus.Loaded;
        }
    }
}
