using Acr.UserDialogs;
using MvvmHelpers.Commands;
using RealEstate.Views;
using System;
using System.Threading.Tasks;

namespace RealEstate.ViewModels
{
    public class PaymentViewModel : BaseViewModel
    {
        string _name;
        string _cardNumber;
        DateTime _date = DateTime.Now.AddYears(1);
        int _cvc;
        bool _isNameValid;
        bool _isCardNumberValid;
        bool _isDateValid;
        bool _isCvcValid;
        bool _isValid;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, onChanged: () => IsNameValid = !string.IsNullOrWhiteSpace(_name));
        }
        public string CardNumber
        {
            get => _cardNumber;
            set => SetProperty(ref _cardNumber, value, onChanged: () => IsCardNumberValid = !string.IsNullOrWhiteSpace(_cardNumber));
        }
        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value, onChanged: () => IsDateValid = _date >= DateTime.Today);
        }
        public int Cvc
        {
            get => _cvc;
            set => SetProperty(ref _cvc, value, onChanged: () => IsCvcValid = (_cvc > 0) && (_cvc <= 999));
        }
        public bool IsNameValid
        {
            get => _isNameValid;
            private set => SetProperty(ref _isNameValid, value, onChanged: UpdateIsValid);
        }
        public bool IsCardNumberValid
        {
            get => _isCardNumberValid;
            private set => SetProperty(ref _isCardNumberValid, value, onChanged: UpdateIsValid);
        }
        public bool IsDateValid
        {
            get => _isDateValid;
            private set => SetProperty(ref _isDateValid, value, onChanged: UpdateIsValid);
        }
        public bool IsCvcValid
        {
            get => _isCvcValid;
            private set => SetProperty(ref _isCvcValid, value, onChanged: UpdateIsValid);
        }
        public bool IsValid
        {
            get => _isValid;
            private set => SetProperty(ref _isValid, value, onChanged: RequestPaymentCommand.RaiseCanExecuteChanged);
        }
        public float Amount { get; private set; }
        public AsyncCommand RequestPaymentCommand { get; private set; }


        public PaymentViewModel()
        {
            RequestPaymentCommand = new AsyncCommand(async () =>
            {
                UserDialogs.Instance.ShowLoading("Initiating transaction...");
                await Task.Delay(2000);
                UserDialogs.Instance.HideLoading();
            }, _ => _isValid);
        }

        public override void Initialize(object navigationData)
        {
            /* var args = navigationData as PaymentPage.Args;
             Amount = args.Amount;*/
            Amount = 250;
            OnPropertyChanged(nameof(Amount));
        }

        void UpdateIsValid()
        {
            IsValid = _isNameValid && _isCardNumberValid && _isDateValid && _isCvcValid;
        }
    }
}
