using Android.Content;
using Android.Content.Res;
using Android.Support.V7.Widget;
using Android.Widget;
using Pokedex.Controls;
using Pokedex.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomCheckBox), typeof(CheckboxRenderer))]
namespace Pokedex.Droid.Renderers
{
    public class CheckboxRenderer : ViewRenderer<CustomCheckBox, AppCompatCheckBox>, CompoundButton.IOnCheckedChangeListener
    {
        private const int DEFAULT_SIZE = 28;

        public CheckboxRenderer(Context context) : base(context)
        {
        }

        public static void Init()
        {
            // intentionally empty
        }

        public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
        {
            ((IViewController)Element).SetValueFromRenderer(CustomCheckBox.IsCheckedProperty, isChecked);
            Element.CheckedCommand?.Execute(Element.CheckedCommandParameter);
        }

        public override SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
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
                    var checkBox = new AppCompatCheckBox(Context);

                    if (Element.OutlineColor != default(Color))
                    {
                        var backgroundColor = GetBackgroundColorStateList(Element.OutlineColor);
                        checkBox.SupportButtonTintList = backgroundColor;
                        checkBox.BackgroundTintList = GetBackgroundColorStateList(Element.InnerColor);
                        checkBox.ForegroundTintList = GetBackgroundColorStateList(Element.OutlineColor);

                    }
                    checkBox.SetOnCheckedChangeListener(this);
                    SetNativeControl(checkBox);
                }

                Control.Checked = e.NewElement.IsChecked;
            }
        }


        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == nameof(Element.IsChecked))
            {
                Control.Checked = Element.IsChecked;
            }
            else
            {
                var backgroundColor = GetBackgroundColorStateList(Element.CheckColor);
                Control.SupportButtonTintList = backgroundColor;
                Control.BackgroundTintList = GetBackgroundColorStateList(Element.InnerColor);
                Control.ForegroundTintList = GetBackgroundColorStateList(Element.OutlineColor);
            }
        }

        private void CheckBoxCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            Element.IsChecked = e.IsChecked;
        }


        private ColorStateList GetBackgroundColorStateList(Color color)
        {
            return new ColorStateList(
                new[]
                {
                new[] {-global::Android.Resource.Attribute.StateEnabled}, // checked
                new[] {-global::Android.Resource.Attribute.StateChecked}, // unchecked
                new[] {global::Android.Resource.Attribute.StateChecked} // checked
                },
                new int[]
                {
                color.WithSaturation(0.1).ToAndroid(),
                color.ToAndroid(),
                color.ToAndroid()
                });
        }

    }
}