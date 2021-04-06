using RealEstate.Models;
using RealEstate.ViewModels;

namespace RealEstate.Views
{
    public partial class SummaryPage : BasePage
    {
        public class Args
        {
            public Rental Rental { get; set; }
        }

        public SummaryPage()
        {
            InitializeComponent();
            BindingContext = new SummaryViewModel();
        }
    }
}
