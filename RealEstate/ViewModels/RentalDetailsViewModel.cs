using MvvmHelpers.Commands;
using RealEstate.Models;
using RealEstate.Views;
using System.Windows.Input;

namespace RealEstate.ViewModels
{
    public class RentalDetailsViewModel : BaseViewModel
    {
        public Rental Rental { get; private set; }
        public ICommand OpenOwnerDetailsCommand { get; private set; }
        public ICommand GoToBookingCommand { get; private set; }

        public RentalDetailsViewModel()
        {
            OpenOwnerDetailsCommand = new AsyncCommand(async () =>
            {
                await _navigationService.GoToPageAsync<RentalOwnerProfilePage>(new RentalOwnerProfilePage.Args
                {
                    Owner = Rental.Owner
                });
            });

            GoToBookingCommand = new AsyncCommand(async () =>
            {
                await _navigationService.GoToPageAsync<SummaryPage>(new SummaryPage.Args
                {
                    Rental = Rental
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
