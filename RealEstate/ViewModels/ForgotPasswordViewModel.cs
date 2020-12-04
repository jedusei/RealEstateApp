using Acr.UserDialogs;
using MvvmHelpers.Commands;
using RealEstate.Views;
using System.Threading.Tasks;

namespace RealEstate.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        int _currentStep;
        string _phoneNumber;
        string _pinCode;
        string _password;
        string _passwordConfirm;
        bool _isPhoneNumberValid;
        bool _isPinCodeValid;
        bool _arePasswordsValid;

        public int CurrentStep
        {
            get => _currentStep;
            set => SetProperty(ref _currentStep, value, onChanged: NextStepCommand.RaiseCanExecuteChanged);
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value, onChanged: () => IsPhoneNumberValid = _phoneNumber?.Length >= 10);
        }
        public string PinCode
        {
            get => _pinCode;
            set => SetProperty(ref _pinCode, value, onChanged: () => IsPinCodeValid = _pinCode?.Length == 5);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, onChanged: () => ArePasswordsValid = (_password?.Length > 0) && (_password == _passwordConfirm));
        }
        public string PasswordConfirm
        {
            get => _passwordConfirm;
            set => SetProperty(ref _passwordConfirm, value, onChanged: () => ArePasswordsValid = (_passwordConfirm?.Length > 0) && (_password == _passwordConfirm));
        }
        public bool IsPhoneNumberValid
        {
            get => _isPhoneNumberValid;
            set => SetProperty(ref _isPhoneNumberValid, value, onChanged: NextStepCommand.RaiseCanExecuteChanged);
        }
        public bool IsPinCodeValid
        {
            get => _isPinCodeValid;
            set => SetProperty(ref _isPinCodeValid, value, onChanged: NextStepCommand.RaiseCanExecuteChanged);
        }
        public bool ArePasswordsValid
        {
            get => _arePasswordsValid;
            set => SetProperty(ref _arePasswordsValid, value, onChanged: NextStepCommand.RaiseCanExecuteChanged);
        }

        public AsyncCommand NextStepCommand { get; private set; }

        public ForgotPasswordViewModel()
        {
            NextStepCommand = new AsyncCommand(NextStepAsync, _ =>
            {
                switch (_currentStep)
                {
                    case 0:
                        return _isPhoneNumberValid;
                    case 1:
                        return _isPinCodeValid;
                    default:
                        return _arePasswordsValid;
                }
            });
        }

        async Task NextStepAsync()
        {
            switch (_currentStep)
            {
                case 0:
                    UserDialogs.Instance.ShowLoading("Please wait...");
                    await Task.Delay(1500); // Send sms code
                    UserDialogs.Instance.HideLoading();
                    break;

                case 1:
                    UserDialogs.Instance.ShowLoading("Please wait...");
                    await Task.Delay(1500); // Verify OTP
                    UserDialogs.Instance.HideLoading();
                    if (_pinCode != "12345")
                    {
                        UserDialogs.Instance.Alert("The PIN code you entered is invalid.", "Error");
                        return;
                    }
                    break;

                default:
                    UserDialogs.Instance.ShowLoading("Please wait...");
                    await Task.Delay(1500); // reset password
                    UserDialogs.Instance.HideLoading();
                    await _navigationService.GoToPageAsync<LoginPage>(clearHistory: true);
                    return;
            }

            CurrentStep++;
        }

        public override bool OnBackButtonPressed()
        {
            if (_currentStep > 0)
            {
                CurrentStep--;
                return true;
            }

            return base.OnBackButtonPressed();
        }
    }
}
