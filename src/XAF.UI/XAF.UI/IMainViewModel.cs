namespace XAF.UI
{
    public interface IMainViewModel : IViewModel
    {
        bool IsBusy { get; set; }

        IViewModel SubViewModel { get; set; }
    }
}