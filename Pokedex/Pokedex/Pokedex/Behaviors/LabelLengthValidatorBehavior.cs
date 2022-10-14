using Xamarin.Forms;

namespace Pokedex.Behaviors
{
    public class LabelLengthValidatorBehavior : Behavior<Label>
    {
        public string Text { get; set; }

        protected override void OnAttachedTo(Label bindable)
        {
            base.OnAttachedTo(bindable);

            Text = bindable.Text;

            if (bindable.Text.Length > 20)
            {
                bindable.Text = Text.Substring(0, 20);
            }
        }

        protected override void OnDetachingFrom(Label bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Text = Text;
        }
    }
}
