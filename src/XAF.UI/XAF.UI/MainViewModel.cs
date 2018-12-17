namespace XAF.UI
{
    public class MainViewModel : ViewModel, IMainViewModel
    {
        private IViewModel _content;
        private bool _isBusy;

        public IViewModel Content
        {
            get => _content;
            set => Set(ref _content, value, () => Content);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value, () => IsBusy);
        }
    }
}