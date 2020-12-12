using Xamarin.Forms;

namespace RealEstate.Models
{
    public enum PaymentPeriod
    {
        Day,
        Week,
        Month,
        Year
    }

    public class Rental : BindableObject
    {
        bool _isFavorite;

        public RentalOwner Owner { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Rating { get; set; }
        public string ImageUrl => (ImageUrls?.Length > 0) ? ImageUrls[0] : null;
        public string[] ImageUrls { get; set; }
        public string Location { get; set; }
        public float Distance { get; set; }
        public float Cost { get; set; }
        public string[] Features { get; set; }
        public PaymentPeriod PaymentPeriod { get; set; }
        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                _isFavorite = value;
                OnPropertyChanged(nameof(IsFavorite));
            }
        }
    }
}
