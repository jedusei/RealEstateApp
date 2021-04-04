using RealEstate.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealEstate.Views
{
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

        private void SfTabView_SelectionChanged(object sender, Syncfusion.XForms.TabView.SelectionChangedEventArgs e)
        {
            if (e.Index == 1)
                _ = scrollView.ScrollToAsync(0, 0, true);
        }
    }
}
