namespace XAF.UI
{
    public interface ICarouselViewModel
    {
        string Image { get; }

        Xamarin.Forms.Command ClickCommand { get; }
    }
}