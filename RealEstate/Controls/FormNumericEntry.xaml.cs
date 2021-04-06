using Xamarin.Forms;

namespace RealEstate.Controls
{
    [System.ComponentModel.DesignTimeVisible(true)]
    public partial class FormNumericEntry : ContentView
    {
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(FormNumericEntry), defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty MinimumProperty = BindableProperty.Create(nameof(Minimum), typeof(double), typeof(FormNumericEntry), double.MinValue);
        public static readonly BindableProperty MaximumProperty = BindableProperty.Create(nameof(Maximum), typeof(double), typeof(FormNumericEntry), double.MaxValue);
        public static readonly BindableProperty MaxDecimalDigitCountProperty = BindableProperty.Create(nameof(MaxDecimalDigitCount), typeof(int), typeof(FormNumericEntry), 6);
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(FormNumericEntry));
        public static readonly BindableProperty FormatStringProperty = BindableProperty.Create(nameof(FormatString), typeof(string), typeof(FormNumericEntry));
        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(FormNumericEntry), ReturnType.Default);
        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(FormNumericEntry));

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
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
        public int MaxDecimalDigitCount
        {
            get => (int)GetValue(MaxDecimalDigitCountProperty);
            set => SetValue(MaxDecimalDigitCountProperty, value);
        }
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
        public string FormatString
        {
            get => (string)GetValue(FormatStringProperty);
            set => SetValue(FormatStringProperty, value);
        }
        public ReturnType ReturnType
        {
            get => (ReturnType)GetValue(ReturnTypeProperty);
            set => SetValue(ReturnTypeProperty, value);
        }
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }


        public FormNumericEntry()
        {
            InitializeComponent();
        }
    }
}