using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Editor), typeof(RealEstate.iOS.Renderers.EditorRenderer))]
namespace RealEstate.iOS.Renderers
{
    class EditorRenderer : Xamarin.Forms.Platform.iOS.EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
                Control.BackgroundColor = UIColor.Clear;
        }
    }
}