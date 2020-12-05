using RealEstate.ViewModels;
using Xamarin.Forms;

namespace RealEstate.Views
{
    public partial class FavoritesTab : ContentView, ITabContentView
    {
        FavoritesViewModel _viewModel;

        public FavoritesTab()
        {
            InitializeComponent();
            BindingContext = _viewModel  = new FavoritesViewModel();
        }

        public void OnStart()
        {
            _viewModel.OnStart();
        }
    }
}