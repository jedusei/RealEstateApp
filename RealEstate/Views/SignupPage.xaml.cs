using RealEstate.ViewModels;
using Xamarin.Forms;

namespace RealEstate.Views
{
    public partial class SignupPage : BasePage
    {
        public SignupPage()
        {
            InitializeComponent();
            ViewModel = new SignupViewModel();
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
