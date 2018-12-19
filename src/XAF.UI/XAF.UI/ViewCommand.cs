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

        public async Task Run()
        {
            await Run(true);
        }

        public async Task Run(bool withBusy)
        {
            var mainViewModel = ServiceLocator.Current.Resolve<IMainViewModel>();
            if (mainViewModel != null && withBusy)
                mainViewModel.IsBusy = true;

            await RunCommand();

            if (mainViewModel != null && withBusy)
                mainViewModel.IsBusy = false;
        }

        protected T CreateViewModel<T>() where T : IViewModel
        {
            return ViewModelFactory.Create<T>();
        }

        protected abstract Task RunCommand();
    }

    public abstract class ViewCommand<T> : IViewCommand<T>
    {
        protected ViewCommand()
        {
            CommandFactory = ServiceLocator.Current.Resolve<ICommandFactory>();
        }

        protected ICommandFactory CommandFactory { get; }

        private IViewModelFactory ViewModelFactory => ServiceLocator.Current.Resolve<IViewModelFactory>();

        public async Task<T> Run()
        {
            return await Run(true);
        }

        public async Task<T> Run(bool withBusy)
        {
            var mainViewModel = ServiceLocator.Current.Resolve<IMainViewModel>();
            if (mainViewModel != null && withBusy)
                mainViewModel.IsBusy = true;

            var result = await RunCommand();

            if (mainViewModel != null && withBusy)
                mainViewModel.IsBusy = false;

            return result;
        }

        protected T CreateViewModel<T>() where T : IViewModel
        {
            return ViewModelFactory.Create<T>();
        }

        protected abstract Task<T> RunCommand();
    }
}