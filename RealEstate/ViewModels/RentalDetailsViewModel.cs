using MvvmHelpers.Commands;
using RealEstate.Models;
using RealEstate.Views;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RealEstate.ViewModels
{
    public class RentalDetailsViewModel : BaseViewModel
    {
        public Rental Rental { get; private set; }
        public ICommand GoBackCommand { get; private set; }
        public ICommand OpenOwnerDetailsCommand { get; private set; }
        public ICommand GoToBookingCommand { get; private set; }

        public RentalDetailsViewModel()
        {
            GoBackCommand = new AsyncCommand(() => _navigationService.GoBackAsync());
        }

        public override Task InitializeAsync(object navigationData)
        {
            var args = navigationData as RentalDetailsPage.Args;
            Rental = args.Rental;
            OnPropertyChanged(nameof(Rental));
            return Task.CompletedTask;
        }
    }
}
