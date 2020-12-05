using Acr.UserDialogs;
using MvvmHelpers.Commands;
using RealEstate.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RealEstate.ViewModels
{
    public class SignupViewModel : BaseViewModel
    {
        int _currentStep;
        string _email;
        string _password;
        string _phoneNumber;
        string _pinCode;
        bool _isEmailValid;
        bool _isPhoneNumberValid;
        bool _isPinCodeValid;

        public int CurrentStep
        {
            get => _currentStep;
            set => SetProperty(ref _currentStep, value, onChanged: NextStepCommand.RaiseCanExecuteChanged);
        }
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value, onChanged: () => IsEmailValid = _email?.Length > 0);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, onChanged: NextStepCommand.RaiseCanExecuteChanged);
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
        public bool IsEmailValid
        {
            get => _isEmailValid;
            set => SetProperty(ref _isEmailValid, value, onChanged: NextStepCommand.RaiseCanExecuteChanged);
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

        public AsyncCommand NextStepCommand { get; private set; }

        public SignupViewModel()
        {
            NextStepCommand = new AsyncCommand(NextStepAsync, _ =>
            {
                switch (_currentStep)
                {
                    case 0:
                        return _isEmailValid && _password?.Length > 0;
                    case 1:
                        return _isPhoneNumberValid;
                    default:
                        return _isPinCodeValid;
                }
            });
        }

        async Task NextStepAsync()
        {
            switch (_currentStep)
            {
                case 0:
                    // Do something with email and password...
                    break;
                case 1:
                    UserDialogs.Instance.ShowLoading("Please wait...");
                    await Task.Delay(1500); // Send sms code
                    UserDialogs.Instance.HideLoading();
                    break;
                default:
                    UserDialogs.Instance.ShowLoading("Please wait...");
                    await Task.Delay(1500); // Send sms code
                    UserDialogs.Instance.HideLoading();
                    if (_pinCode != "12345")
                        UserDialogs.Instance.Alert("The PIN code you entered is invalid.", "Error");
                    else
                        await _navigationService.GoToPageAsync<MainPage>(clearHistory: true);
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
