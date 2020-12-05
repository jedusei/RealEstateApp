using RealEstate.Models;
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
                ImageUrl = "https://webtekdi.com/website/buynow/templates/IBGWTCT013/IBGWTCT013TMPL001/images/1.jpg",
                Location = "Los Angeles",
                Distance = 2.3,
                Cost = 250,
                PaymentPeriod = PaymentPeriod.Day
            },
            new Rental
            {
                Title = "Sunny Apartment",
                ImageUrl = "https://webtekdi.com/website/buynow/templates/IBGWTCT013/IBGWTCT013TMPL001/images/1.jpg",
                Location = "Los Angeles",
                Distance = 2.3,
                Cost = 250,
                PaymentPeriod = PaymentPeriod.Day
            }
        };

        public async Task<Rental[]> GetRentalsAsync()
        {
            await Task.Delay(2000);
            return _rentals;
        }
    }
}
