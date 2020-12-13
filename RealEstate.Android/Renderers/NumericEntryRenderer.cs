using RealEstate.Controls;
using Syncfusion.SfNumericTextBox.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NumericEntry), typeof(RealEstate.Droid.Renderers.NumericEntryRenderer))]
namespace RealEstate.Droid.Renderers
{
    class NumericEntryRenderer : Syncfusion.SfNumericTextBox.XForms.Droid.SfNumericTextBoxRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SfNumericTextBox> e)
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