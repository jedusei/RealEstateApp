using RealEstate.ViewModels;
using Xamarin.Forms;

namespace RealEstate.Views
{
    public partial class MessagesTab : ContentView, ITabContentView
    {
        MessagesViewModel _viewModel;

        public MessagesTab()
        {
            InitializeComponent();
            BindingContext = _viewModel  = new MessagesViewModel();
        }

        public void OnStart()
        {
            _viewModel.OnStart();
        }
    }
}