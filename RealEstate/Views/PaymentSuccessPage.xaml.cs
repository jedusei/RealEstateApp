using RealEstate.ViewModels;

namespace RealEstate.Views
{
    public partial class PaymentSuccessPage : BasePage
    {
        public PaymentSuccessPage()
        {
            InitializeComponent();
            BindingContext = new PaymentSuccessViewModel();
        }
    }
}
