using System.Collections.Generic;
using System.ComponentModel;
using XAF.Command;

namespace XAF.UI
{
    public interface IViewModel : INotifyPropertyChanged
    {
        ICommandFactory CommandFactory { get; }

        void RegisterViewModelDescriptor(IViewModelDescriptor viewModelDescriptor);

        void RegisterViewModelDescriptors(IList<IViewModelDescriptor> viewModelDescriptors);

        IViewModel CreateViewModel<T>() where T : IViewModel;
    }
}