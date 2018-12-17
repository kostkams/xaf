using System;
using System.Threading.Tasks;
using Autofac;
using Xamarin.Forms;
using XAF.Autofac;
using XAF.Command;

namespace XAF.UI
{
    public abstract class ViewCommand : IViewCommand
    {
        protected ViewCommand()
        {
            CommandFactory = ServiceLocator.Current.Resolve<ICommandFactory>();
        }

        protected ICommandFactory CommandFactory { get; }


        private IViewModelFactory ViewModelFactory => ServiceLocator.Current.Resolve<IViewModelFactory>();

        public void Run(Action action)
        {
            var mainViewModel = ServiceLocator.Current.Resolve<IMainViewModel>();
            if (mainViewModel != null)
                mainViewModel.IsBusy = true;

            Task.Run(() => { RunCommand(); })
                .ContinueWith(task =>
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        action?.Invoke();

                        if (mainViewModel != null)
                            mainViewModel.IsBusy = false;
                    }));
        }

        protected T CreateViewModel<T>() where T : IViewModel
        {
            return ViewModelFactory.Create<T>();
        }

        protected abstract void RunCommand();
    }

    public abstract class ViewCommand<T> : IViewCommand<T>
    {
        protected ViewCommand()
        {
            CommandFactory = ServiceLocator.Current.Resolve<ICommandFactory>();
        }

        protected ICommandFactory CommandFactory { get; }

        private IViewModelFactory ViewModelFactory => ServiceLocator.Current.Resolve<IViewModelFactory>();

        public void Run(Action<T> action)
        {
            var mainViewModel = ServiceLocator.Current.Resolve<IMainViewModel>();
            if (mainViewModel != null)
                mainViewModel.IsBusy = true;

            Task.Run(() => RunCommand())
                .ContinueWith(task =>
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        action?.Invoke(task.Result);

                        if (mainViewModel != null)
                            mainViewModel.IsBusy = false;
                    }));
        }

        protected T CreateViewModel<T>() where T : IViewModel
        {
            return ViewModelFactory.Create<T>();
        }

        protected abstract T RunCommand();
    }
}