using RealEstate.ViewModels;
using Xamarin.Forms;

namespace RealEstate.Views
{
    public partial class HomeTab : ContentView, ITabContentView
    {
        HomeViewModel _viewModel;

        public HomeTab()
        {
            InitializeComponent();
            BindingContext = _viewModel  = new HomeViewModel();
        }

        public void OnStart()
        {
            _viewModel.OnStart();
        }
    }
}