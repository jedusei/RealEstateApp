﻿using RealEstate.Models;
using RealEstate.ViewModels;

namespace RealEstate.Views
{
    public partial class RentalDetailsPage : BasePage
    {
        public class Args
        {
            public Rental Rental { get; set; }
        }

        public RentalDetailsPage()
        {
            InitializeComponent();
            BindingContext = new RentalDetailsViewModel();
        } 
    }
}
