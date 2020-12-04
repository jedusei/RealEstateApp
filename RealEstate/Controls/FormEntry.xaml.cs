using Xamarin.Forms;

namespace RealEstate.Controls
{
    public partial class FormEntry : ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(FormEntry), defaultBindingMode: BindingMode.TwoWay);
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(FormEntry));
        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(FormEntry), Keyboard.Default);
        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(nameof(ReturnType), typeof(ReturnType), typeof(FormEntry), ReturnType.Default);
        public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(FormEntry));
        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(FormEntry), int.MaxValue);
        public static readonly BindableProperty IsValidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(FormEntry));
        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(FormEntry));
        public static readonly BindableProperty IsPasswordVisibleProperty = BindableProperty.Create(nameof(IsPasswordVisible), typeof(bool), typeof(FormEntry));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }
        public Keyboard Keyboard
        {
            get => (Keyboard)GetValue(KeyboardProperty);
            set => SetValue(KeyboardProperty, value);
        }
        public ReturnType ReturnType
        {
            get => (ReturnType)GetValue(ReturnTypeProperty);
            set => SetValue(ReturnTypeProperty, value);
        }
        public double CharacterSpacing
        {
            get => (double)GetValue(CharacterSpacingProperty);
            set => SetValue(CharacterSpacingProperty, value);
        }
        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }
        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }
        public bool IsPasswordVisible
        {
            get => (bool)GetValue(IsPasswordVisibleProperty);
            set => SetValue(IsPasswordVisibleProperty, value);
        }


        public FormEntry()
        {
            InitializeComponent();
        }

        void PasswordVisiblityToggle_Tapped(object sender, System.EventArgs e)
        {
            IsPasswordVisible = !IsPasswordVisible;
        }
    }
}