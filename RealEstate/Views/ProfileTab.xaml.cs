using RealEstate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstate.Views
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class ProfileTab : ContentView, ITabContentView
    {
        ProfileViewModel _viewModel;

        public ProfileTab()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ProfileViewModel();
        }

        public void OnStart()
        {
            _viewModel.OnStart();
        }

        async void CarouselView_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            if (e.CurrentPosition == 0)
                scrollView.IsEnabled = true;
            else
            {
                scrollView.IsEnabled = false;
                await scrollView.ScrollToAsync(0, 0, true);
            }
        }
    }
}
