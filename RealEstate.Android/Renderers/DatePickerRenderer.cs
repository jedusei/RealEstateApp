using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DatePicker), typeof(RealEstate.iOS.Renderers.DatePickerRenderer))]
namespace RealEstate.iOS.Renderers
{
    class DatePickerRenderer : Xamarin.Forms.Platform.Android.DatePickerRenderer
    {
        public DatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                Control.SetPadding(0, 0, 0, 0);
            }
        }
    }
}