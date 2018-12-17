using Xamarin.Forms;

namespace XAF.UI
{
    public interface ICarouselViewModel
    {
        string Image { get; }
        
        Color BackgroundColor { get; }

        Color TextColor { get; }

        Xamarin.Forms.Command ClickCommand { get; }
    }
}