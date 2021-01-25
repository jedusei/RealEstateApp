using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(RealEstate.iOS.Renderers.EntryRenderer))]
namespace RealEstate.iOS.Renderers
{
    class EntryRenderer : Xamarin.Forms.Platform.iOS.EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
                Control.BackgroundColor = UIColor.Clear;
            }
        }
    }
}