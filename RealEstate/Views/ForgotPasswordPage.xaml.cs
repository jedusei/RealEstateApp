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
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            SetImageHeight();
        }
        protected override void OnRefresh()
        {
            base.OnRefresh();
            SetImageHeight();
        }
        void SetImageHeight()
        {
            var imageHeight = MaxSize.Height - content.Height + content.CornerRadius.Left;
            if (AbsoluteLayout.GetLayoutBounds(backgroundImage).Height != imageHeight)
                AbsoluteLayout.SetLayoutBounds(backgroundImage, new Rectangle(0, 0, 1, imageHeight));
        }
    }
}
