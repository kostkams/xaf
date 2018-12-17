namespace XAF.UI
{
    public class MainViewModel : ViewModel, IMainViewModel
    {
        private bool _isBusy;
        private IViewModel _subViewModel;

        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value, () => IsBusy);
        }

        public IViewModel SubViewModel
        {
            get => _subViewModel;
            set => Set(ref _subViewModel, value, () => SubViewModel);
        }
    }
}