using RealEstate.ViewModels;
using Xamarin.Forms;

namespace RealEstate.Views
{
    public partial class ForgotPasswordPage : BasePage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();
            ViewModel = new ForgotPasswordViewModel();
        }
        protected override void OnStart()
        {
            base.OnStart();
            SetImageHeight();
        }
        protected override void OnRefresh()
        {
            base.OnRefresh();
            SetImageHeight();
        }
        void SetImageHeight()
        {
            AbsoluteLayout.SetLayoutBounds(backgroundImage, new Rectangle(0, 0, 1, content.Y + 20));
        }
    }
}
