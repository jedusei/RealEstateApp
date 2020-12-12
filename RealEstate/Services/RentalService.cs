using RealEstate.Models;
using System.Collections.ObjectModel;
using System.Linq;
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
                ImageUrls = new[] {
                    "https://webtekdi.com/website/buynow/templates/IBGWTCT013/IBGWTCT013TMPL001/images/1.jpg",
                    "https://www.supernamai.lt/wp-content/images/ebsiqvhpalepe%20daugiabucio%20dalis.jpg",
                    "background_1.jpg"
                },
                Location = "Los Angeles",
                Distance = 2.3f,
                Cost = 250,
                Rating = 5,
                PaymentPeriod = PaymentPeriod.Day,
                Features = new[] {
                    "3 Bathrooms",
                    "4 Bedrooms",
                    "WiFi",
                    "Swimming pool"
                }
            },
            new Rental
            {
                Title = "Sunny Apartment",
                ImageUrls = new[] { "https://www.supernamai.lt/wp-content/images/ebsiqvhpalepe%20daugiabucio%20dalis.jpg" },
                Location = "Los Angeles",
                Distance = 2.3f,
                Cost = 200,
                Rating = 4.5f,
                PaymentPeriod = PaymentPeriod.Day,
                Features = new[] {
                    "2 Bedrooms",
                    "WiFi"
                }
            },
            new Rental
            {
                Title = "Sunny Apartment",
                ImageUrls = new[] { "background_1.jpg" },
                Location = "Los Angeles",
                Distance = 2.3f,
                Cost = 230,
                Rating = 4,
                PaymentPeriod = PaymentPeriod.Day,
                Features = new[] {
                    "3 Bedrooms",
                    "Swimming pool"
                }
            }
        };

        ObservableCollection<Rental> _favouriteRentals = new ObservableCollection<Rental>();
        ObservableCollection<Rental> _rentalHistory;

        public RentalService()
        {
            _rentalHistory = new ObservableCollection<Rental>(_rentals);

            var owner = new RentalOwner
            {
                Name = "Jack Nicolson",
                Email = "abc@gmail.com",
                PhoneNumber = "0201234567",
                Location = "Los Angeles",
                BannerImageUrl = "https://static1.sibcycline.com/photos/e0/da/7/e0da76a7fbcd83f59ee3ae32abaed6ad56da2238_1024.jpg",
                ProfileImageUrl = "https://darylh.com/wp-content/uploads/2017/05/Profile-Picture-Square.jpg",
                Rating = 5,
                ReviewCount = 1438,
                Languages = new[] { "English", "Deutsch", "Russian" },
                Description = "I am Jack and I have 3 apartments for rent short and long term, I invite tenants who appreciate the convenience of use and nice aesthetic interiors."
            };

            var description = Application.Current.Resources["LongDummyText"] as string;

            foreach (var rental in _rentals)
            {
                rental.Owner = owner;
                rental.Description = description;
                rental.PropertyChanged += OnRentalPropertyChanged;
            }
        }

        void OnRentalPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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

        public async Task<Rental[]> GetRentalsAsync(RentalOwner owner)
        {
            await Task.Delay(2000);
            if (owner == null)
                return _rentals;
            else
                return _rentals.Where(r => r.Owner == owner).ToArray();
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
