﻿using RealEstate.Controls;
using Syncfusion.SfNumericTextBox.XForms;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NumericEntry), typeof(RealEstate.iOS.Renderers.NumericEntryRenderer))]
namespace RealEstate.iOS.Renderers
{
    class NumericEntryRenderer : Syncfusion.SfNumericTextBox.XForms.iOS.SfNumericTextBoxRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SfNumericTextBox> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.LayoutMargins = new UIEdgeInsets();
              //  Control.BorderStyle = UITextBorderStyle.None;
                Control.BackgroundColor = UIColor.Clear;
            }
        }
    }
}