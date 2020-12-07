using RealEstate.ViewModels;
using Xamarin.Forms;

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
    }
}
