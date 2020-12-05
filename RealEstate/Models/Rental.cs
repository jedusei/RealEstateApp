using System;
using System.Collections.Generic;
using System.Text;
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

        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }
        public double Distance { get; set; }
        public double Cost { get; set; }
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
