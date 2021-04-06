using Acr.UserDialogs;
using MvvmHelpers.Commands;
using RealEstate.Views;
using System.Threading.Tasks;
using System.Windows.Input;

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
            set => SetProperty(ref _isEmailValid, value, onChanged: NextCommand.RaiseCanExecuteChanged);
        }
        public bool IsPasswordValid
        {
            get => _isPasswordValid;
            set => SetProperty(ref _isPasswordValid, value, onChanged: LoginCommand.RaiseCanExecuteChanged);
        }

        public Command NextCommand { get; set; }
        public ICommand SignupCommand { get; set; }
        public AsyncCommand LoginCommand { get; set; }
        public ICommand ForgotPasswordCommand { get; set; }


        public LoginViewModel()
        {
            NextCommand = new Command(() => CurrentPage = 1, () => IsEmailValid);
            LoginCommand = new AsyncCommand(LoginAsync, _ => IsPasswordValid);
            SignupCommand = new AsyncCommand(async () =>
            {
                await _navigationService.GoToPageAsync<SignupPage>();
                await Task.Delay(BasePage.TRANSITION_DURATION);
                Reset();
            });
            ForgotPasswordCommand = new AsyncCommand(async () =>
            {
                await _navigationService.GoToPageAsync<ForgotPasswordPage>();
                await Task.Delay(BasePage.TRANSITION_DURATION);
                Reset();
            });
        }

        void Reset()
        {
            CurrentPage = 0;
            Email = Password = null;
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

        async Task LoginAsync()
        {
            UserDialogs.Instance.ShowLoading("Logging in...");
            await Task.Delay(1500);
            UserDialogs.Instance.HideLoading();
            await _navigationService.GoToPageAsync<MainPage>(clearHistory: true);
        }

    }
}
