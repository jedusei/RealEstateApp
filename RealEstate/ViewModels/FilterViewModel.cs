using MvvmHelpers.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;

namespace RealEstate.ViewModels
{
    public class FilterViewModel : BaseModalViewModel
    {
        List<DateTime> _selectedDates = new List<DateTime>();
        double _priceRangeStart = 0;
        double _priceRangeEnd = 10000;
        IList _selectedRoomTypes;

        public List<DateTime> SelectedDates
        {
            get => _selectedDates;
            set => SetProperty(ref _selectedDates, value);
        }
        public double PriceRangeStart
        {
            get => _priceRangeStart;
            set => SetProperty(ref _priceRangeStart, value);
        }
        public double PriceRangeEnd
        {
            get => _priceRangeEnd;
            set => SetProperty(ref _priceRangeEnd, value);
        }
        public IList SelectedRoomTypes
        {
            get => _selectedRoomTypes;
            set => SetProperty(ref _selectedRoomTypes, value);
        }
        public bool[] SelectedRatings { get; } = new bool[] { true, true, true, true, true };
        public ICommand ToggleRatingFilter { get; private set; }
        public ICommand ApplyCommand { get; private set; }

        public FilterViewModel(Action<object> setResultAction) : base(setResultAction)
        {
            ToggleRatingFilter = new Xamarin.Forms.Command<int>(rating =>
            {
                SelectedRatings[rating - 1] ^= true;
                OnPropertyChanged(nameof(SelectedRatings));
            });

            ApplyCommand = new AsyncCommand((GoBackCommand as AsyncCommand).ExecuteAsync);
        }
    }
}
