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
            OpenOwnerDetailsCommand = new AsyncCommand(async () =>
            {
                await _navigationService.GoToPageAsync<RentalOwnerProfilePage>(new RentalOwnerProfilePage.Args
                {
                    Owner = Rental.Owner
                });
            });
        }

        public override void Initialize(object navigationData)
        {
            var args = navigationData as RentalDetailsPage.Args;
            Rental = args.Rental;
            OnPropertyChanged(nameof(Rental));
        }

        public override void OnResume(object navigationData = null)
        {
            base.OnResume(navigationData);
            if (navigationData != null)
                Initialize(navigationData);
        }
    }
}
