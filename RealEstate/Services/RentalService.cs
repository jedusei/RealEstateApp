using RealEstate.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(RealEstate.Services.RentalService))]
namespace RealEstate.Services
{
    class RentalService : IRentalService
    {
        Rental[] _rentals = new Rental[]
        {
            new Rental
            {
                Title = "Sunny Apartment",
                ImageUrl = "https://webtekdi.com/website/buynow/templates/IBGWTCT013/IBGWTCT013TMPL001/images/1.jpg",
                Location = "Los Angeles",
                Distance = 2.3,
                Cost = 250,
                PaymentPeriod = PaymentPeriod.Day
            },
            new Rental
            {
                Title = "Sunny Apartment",
                ImageUrl = "https://www.supernamai.lt/wp-content/images/ebsiqvhpalepe%20daugiabucio%20dalis.jpg",
                Location = "Los Angeles",
                Distance = 2.3,
                Cost = 250,
                PaymentPeriod = PaymentPeriod.Day
            },
            new Rental
            {
                Title = "Sunny Apartment",
                ImageUrl = "background_1.jpg",
                Location = "Los Angeles",
                Distance = 2.3,
                Cost = 250,
                PaymentPeriod = PaymentPeriod.Day
            }
        };

        ObservableCollection<Rental> _favouriteRentals = new ObservableCollection<Rental>();
        ObservableCollection<Rental> _rentalHistory  ;

        public RentalService()
        {
            _rentalHistory = new ObservableCollection<Rental>(_rentals);
            foreach (var rental in _rentals)
                rental.PropertyChanged += OnRentalPropertyChanged;
        }

        private void OnRentalPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Rental.IsFavorite))
            {
                var rental = sender as Rental;
                if (rental.IsFavorite)
                    _favouriteRentals.Add(rental);
                else
                    _favouriteRentals.Remove(rental);
            }
        }

        public async Task<Rental[]> GetRentalsAsync()
        {
            await Task.Delay(2000);
            return _rentals;
        }

        public async Task<ObservableCollection<Rental>> GetFavoriteRentalsAsync()
        {
            await Task.Delay(1000);
            return _favouriteRentals;
        }

        public async Task<ObservableCollection<Rental>> GetRentalHistoryAsync()
        {
            await Task.Delay(1000);
            return _rentalHistory;
        }
    }
}
