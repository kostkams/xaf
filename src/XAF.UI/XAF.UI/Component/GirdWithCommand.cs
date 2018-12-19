using Xamarin.Forms;

namespace XAF.UI.Component
{
    public class GirdWithCommand : Grid
    {
        public GirdWithCommand()
        {
            var gestureRecognizer = new TapGestureRecognizer();

            gestureRecognizer.Tapped += (s, e) => {
                if (Command != null && Command.CanExecute(null))
                {
                    Command.Execute(null);
                }
            };

            GestureRecognizers.Add(gestureRecognizer);
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create( nameof(Command), typeof(Xamarin.Forms.Command), typeof(GirdWithCommand));

        public Xamarin.Forms.Command Command
        {
            get => (Xamarin.Forms.Command)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}