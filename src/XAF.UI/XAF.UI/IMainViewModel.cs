namespace XAF.UI
{
    public interface IMainViewModel : IViewModel
    {
        IViewModel Content { get; set; }

        bool IsBusy { get; set; }
    }
}