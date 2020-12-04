using RealEstate.ViewModels;

namespace RealEstate.Views
{
    public partial class LoginPage : BasePage
    {
        public LoginPage()
        {
            InitializeComponent();
            ViewModel = new LoginViewModel();
        }
    }
}
