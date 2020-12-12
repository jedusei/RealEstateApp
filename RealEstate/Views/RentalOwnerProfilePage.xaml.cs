using RealEstate.Models;
using RealEstate.ViewModels;
using Xamarin.Forms;

namespace RealEstate.Views
{
    [SingletonPage]
    public partial class RentalOwnerProfilePage : BasePage
    {
        public class Args
        {
            public RentalOwner Owner;
        }

        RentalOwnerProfileViewModel _viewModel;

        public RentalOwnerProfilePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new RentalOwnerProfileViewModel();
        }

        protected override void OnStart()
        {
            base.OnStart();
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
