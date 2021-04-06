using RealEstate.ViewModels;

namespace RealEstate.Views
{
    public partial class FilterModal : BottomSheetModal
    {
        public FilterModal()
        {
            InitializeComponent();
            BindingContext = new FilterViewModel(x => { });
        }
    }
}
