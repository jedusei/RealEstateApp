using RealEstate.ViewModels;

namespace RealEstate.Views
{
    public partial class PaymentPage : BasePage
    {
        public class Args
        {
            public float Amount { get; set; }
        }

        public PaymentPage()
        {
            InitializeComponent();
            BindingContext = new PaymentViewModel();
        }
    }
}
