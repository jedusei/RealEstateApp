using MvvmHelpers.Commands;
using RealEstate.Models;
using RealEstate.Views;
using System.Windows.Input;

namespace RealEstate.ViewModels
{
    public class SummaryViewModel : BaseViewModel
    {
        int _nightCount;
        float _rentCharge;
        float _serviceCharge = 25;
        float _totalCost;

        public Rental Rental { get; private set; }
        public int NightCount
        {
            get => _nightCount;
            set => SetProperty(ref _nightCount, value, onChanged: () => RentCost = Rental.Cost * _nightCount);
        }
        public float RentCost
        {
            get => _rentCharge;
            private set => SetProperty(ref _rentCharge, value, onChanged: UpdateTotalCost);
        }
        public float ServiceCharge
        {
            get => _serviceCharge;
            set => SetProperty(ref _serviceCharge, value, onChanged: UpdateTotalCost);
        }
        public float TotalCost
        {
            get => _totalCost;
            private set => SetProperty(ref _totalCost, value);
        }
        public ICommand GoToPaymentCommand { get; private set; }

        public SummaryViewModel()
        {
            GoToPaymentCommand = new AsyncCommand(() => _navigationService.GoToPageAsync<PaymentPage>());
        }

        public override void Initialize(object navigationData)
        {
            var args = navigationData as SummaryPage.Args;
            Rental = args.Rental;
            OnPropertyChanged(nameof(Rental));
            NightCount = 3;
        }

        void UpdateTotalCost()
        {
            TotalCost = _rentCharge + _serviceCharge;
        }
    }
}
