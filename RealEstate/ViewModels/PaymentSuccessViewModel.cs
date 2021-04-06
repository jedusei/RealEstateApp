using MvvmHelpers.Commands;
using System.Windows.Input;

namespace RealEstate.ViewModels
{
    public class PaymentSuccessViewModel : BaseViewModel
    {
        public ICommand GoHomeCommand { get; private set; }

        public PaymentSuccessViewModel()
        {
            GoHomeCommand = new AsyncCommand(() => _navigationService.PopToRootAsync());
        }

        public override bool OnBackButtonPressed()
        {
            // Prevent user from going back
            return true;
        }
    }
}
