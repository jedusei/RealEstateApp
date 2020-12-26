using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace RealEstate.Controls
{
    public partial class RangeSlider : ContentView
    {
        public static readonly BindableProperty MinimumProperty = BindableProperty.Create(nameof(Minimum), typeof(double), typeof(RangeSlider), 0.0);
        public static readonly BindableProperty MaximumProperty = BindableProperty.Create(nameof(Maximum), typeof(double), typeof(RangeSlider), 100.0);
        public static readonly BindableProperty ValueStartProperty = BindableProperty.Create(nameof(ValueStart), typeof(double), typeof(RangeSlider), 25.0, defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty ValueEndProperty = BindableProperty.Create(nameof(ValueEnd), typeof(double), typeof(RangeSlider), 75.0, defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty LabelFormatProperty = BindableProperty.Create(nameof(LabelFormat), typeof(string), typeof(RangeSlider), "F0");

        bool _isGestureRunning;

        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }
        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }
        public double ValueStart
        {
            get => (double)GetValue(ValueStartProperty);
            set => SetValue(ValueStartProperty, value);
        }
        public double ValueEnd
        {
            get => (double)GetValue(ValueEndProperty);
            set => SetValue(ValueEndProperty, value);
        }
        public string LabelFormat
        {
            get => (string)GetValue(LabelFormatProperty);
            set => SetValue(LabelFormatProperty, value);
        }

        public RangeSlider()
        {
            InitializeComponent();

            var panGestureRecognizer = minThumbHandle.GestureRecognizers[0] as PanGestureRecognizer;
            panGestureRecognizer.PanUpdated += MinThumb_PanUpdated;
            panGestureRecognizer = maxThumbHandle.GestureRecognizers[0] as PanGestureRecognizer;
            panGestureRecognizer.PanUpdated += MaxThumb_PanUpdated;

            Content.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Content.Width))
                    UpdateArrangement();
            };
        }

        void UpdateArrangement()
        {
            var minX = (ValueStart - Minimum) / (Maximum - Minimum);
            var bounds = AbsoluteLayout.GetLayoutBounds(minThumb);
            bounds.X = minX;
            AbsoluteLayout.SetLayoutBounds(minThumb, bounds);

            var maxX = (ValueEnd - Minimum) / (Maximum - Minimum);
            bounds = AbsoluteLayout.GetLayoutBounds(maxThumb);
            bounds.X = maxX;
            AbsoluteLayout.SetLayoutBounds(maxThumb, bounds);

            bounds = AbsoluteLayout.GetLayoutBounds(rangeTrack);
            bounds.Width = maxX - minX;
            bounds.X = minX / (1 - bounds.Width);
            AbsoluteLayout.SetLayoutBounds(rangeTrack, bounds);

            var minThumbEllipse = minThumb.Children[0];
            minThumbEllipse.TranslationX = (ValueStart != Minimum) ? 0 : 0.5 * (minThumbEllipse.Width - minThumb.Width);
            var maxThumbEllipse = maxThumb.Children[0];
            maxThumbEllipse.TranslationX = (ValueEnd != Maximum) ? 0 : 0.5 * (maxThumb.Width - maxThumbEllipse.Width);

            if (!_isGestureRunning)
                ResetThumbHandles();
        }

        void ResetThumbHandles()
        {
            var bounds = AbsoluteLayout.GetLayoutBounds(minThumbHandle);
            bounds.X = (ValueStart - Minimum) / (Maximum - Minimum);
            AbsoluteLayout.SetLayoutBounds(minThumbHandle, bounds);

            bounds = AbsoluteLayout.GetLayoutBounds(maxThumbHandle);
            bounds.X = (ValueEnd - Minimum) / (Maximum - Minimum);
            AbsoluteLayout.SetLayoutBounds(maxThumbHandle, bounds);
        }

        void MinThumb_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    _isGestureRunning = true;
                    break;

                case GestureStatus.Running:
                    var handleCenterX = minThumbHandle.X + minThumbHandle.Width * 0.5;
                    var newValue = Minimum + ((Maximum - Minimum) * (handleCenterX + e.TotalX) / Content.Width);
                    ValueStart = Math.Clamp(newValue, Minimum, ValueEnd);
                    break;

                default:
                    _isGestureRunning = false;
                    ResetThumbHandles();
                    break;
            }
        }
        void MaxThumb_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    _isGestureRunning = true;
                    break;

                case GestureStatus.Running:
                    var handleCenterX = maxThumbHandle.X + maxThumbHandle.Width * 0.5;
                    var newValue = Minimum + ((Maximum - Minimum) * (handleCenterX + e.TotalX) / Content.Width);
                    ValueEnd = Math.Clamp(newValue, ValueStart, Maximum);
                    break;

                default:
                    _isGestureRunning = false;
                    ResetThumbHandles();
                    break;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName)
            {
                case nameof(Minimum):
                case nameof(Maximum):
                case nameof(ValueStart):
                case nameof(ValueEnd):
                    UpdateArrangement();
                    break;
            }
        }
    }
}