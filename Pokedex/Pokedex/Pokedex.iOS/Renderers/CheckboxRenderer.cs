﻿using System;
using Pokedex.Controls;
using Pokedex.iOS.Renderers;
using SaturdayMP.XPlugins.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomCheckBox), typeof(CheckboxRenderer))]
namespace Pokedex.iOS.Renderers
{
    public class CheckboxRenderer : ViewRenderer<CustomCheckBox, BEMCheckBox>
    {
        private const int DEFAULT_SIZE = 28;

        public static void Initialize()
        {
        }

        public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            var sizeConstraint = base.GetDesiredSize(widthConstraint, heightConstraint);

            if (sizeConstraint.Request.Width == 0)
            {
                var width = widthConstraint;
                if (widthConstraint <= 0)
                {
                    System.Diagnostics.Debug.WriteLine("Default values");
                    width = DEFAULT_SIZE;
                }
                else if (widthConstraint <= 0)
                {
                    width = DEFAULT_SIZE;
                }

                sizeConstraint = new SizeRequest(new Size(width, sizeConstraint.Request.Height),
                    new Size(width, sizeConstraint.Minimum.Height));
            }

            return sizeConstraint;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CustomCheckBox> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    var checkBox = new BEMCheckBox();

                    checkBox.BoxType = BEMBoxType.Square;
                    checkBox.OnAnimationType = BEMAnimationType.Fill;
                    checkBox.OffAnimationType = BEMAnimationType.Fill;

                    UpdateColors(checkBox);

                    SetNativeControl(checkBox);
                }

                Control.On = e.NewElement.IsChecked;
                Control.ValueChanged += Control_ValueChanged;
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == nameof(Element.IsChecked))
            {
                Control.On = Element.IsChecked;
            }
            else
            {
                UpdateColors(Control);
            }
        }

        void Control_ValueChanged(object sender, EventArgs e)
        {
            Element.IsChecked = Control.On;
            Element.CheckedCommand?.Execute(Element.CheckedCommandParameter);
        }

        private void UpdateColors(BEMCheckBox nativeCheckBox)
        {
            if (nativeCheckBox != null)
            {
                nativeCheckBox.TintColor = Element.OutlineColor.ToUIColor();
                nativeCheckBox.OffFillColor = Element.InnerColor.ToUIColor();
                nativeCheckBox.OnFillColor = Element.CheckedInnerColor.ToUIColor();
                nativeCheckBox.OnTintColor = Element.CheckedOutlineColor.ToUIColor();
                nativeCheckBox.OnCheckColor = Element.CheckColor.ToUIColor();
            }
        }
    }
}
