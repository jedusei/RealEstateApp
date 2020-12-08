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
        int _currentTab;

        public User User => _userService.User;
        public int CurrentTab { get => _currentTab;
            set => SetProperty(ref _currentTab, value); }
        public ObservableCollection<Rental> RentalHistory { get; private set; }
        public ICommand GoToTabCommand { get; set; }
        public ICommand ToggleFavoriteCommand { get; private set; }
        public ICommand OpenSettingsCommand { get; private set; }


        public ProfileViewModel()
        {
            _userService = DependencyService.Get<IUserService>();
            _rentalService = DependencyService.Get<IRentalService>();
            _loadStatus = LoadStatus.Loading;
            GoToTabCommand = new Command<int>(tab => CurrentTab = tab);
            ToggleFavoriteCommand = new Command<Rental>(rental => rental.IsFavorite = !rental.IsFavorite);
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
