using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DatePicker), typeof(RealEstate.iOS.Renderers.DatePickerRenderer))]
namespace RealEstate.iOS.Renderers
{
    class DatePickerRenderer : Xamarin.Forms.Platform.iOS.DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
                Control.BackgroundColor = Color.Transparent.ToUIColor();
        }
    }
}