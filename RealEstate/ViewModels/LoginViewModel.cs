using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RealEstate.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        int _currentPage;
        string _email;
        string _password;
        bool _isEmailValid;
        bool _isPasswordValid;

        public int CurrentPage
        {
            get => _currentPage;
            private set => SetProperty(ref _currentPage, value);
        }
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value, onChanged: () => IsEmailValid = _email?.Length > 0);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, onChanged: () => IsPasswordValid = _password?.Length > 0);
        }

        public bool IsEmailValid
        {
            get => _isEmailValid;
            set => SetProperty(ref _isEmailValid, value, onChanged: (NextCommand as Command).ChangeCanExecute);
        }
        public bool IsPasswordValid
        {
            get => _isPasswordValid;
            set => SetProperty(ref _isPasswordValid, value, onChanged: (LoginCommand as Command).ChangeCanExecute);
        }

        public ICommand NextCommand { get; set; }
        public ICommand SignupCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }


        public LoginViewModel()
        {
            NextCommand = new Command(() => CurrentPage = 1, () => IsEmailValid);
        }

        public override bool OnBackButtonPressed()
        {
            if (CurrentPage == 1)
            {
                CurrentPage = 0;
                return true;
            }

            return base.OnBackButtonPressed();
        }

    }
}
