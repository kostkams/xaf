using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Autofac;
using XAF.Autofac;
using XAF.Command;

namespace XAF.UI
{
    public abstract class ViewModel : IViewModel
    {
        protected ViewModel()
        {
            ViewModelDescriptors = new List<IViewModelDescriptor>();
            CommandFactory = ServiceLocator.Current.Resolve<ICommandFactory>();
        }

        private IList<IViewModelDescriptor> ViewModelDescriptors { get; }
        

        public void RegisterViewModelDescriptor(IViewModelDescriptor viewModelDescriptor)
        {
            ViewModelDescriptors.Add(viewModelDescriptor);
        }

        public void RegisterViewModelDescriptors(IList<IViewModelDescriptor> viewModelDescriptors)
        {
            ((List<IViewModelDescriptor>)ViewModelDescriptors).AddRange(viewModelDescriptors);
        }

        public ICommandFactory CommandFactory { get; }


        public IViewModel CreateViewModel<T>() where T : IViewModel
        {
            return ViewModelFactory.Create<T>();
        }

        private IViewModelFactory ViewModelFactory => ServiceLocator.Current.Resolve<IViewModelFactory>();

        protected void Set<T>(ref T value, T newValue, Expression<Func<T>> property)
        {
            if (Equals(value, newValue))
                return;

            value = newValue;
            OnPropertyChanged(property);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Notifies subscribers of the property change.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="property">The property expression.</param>
        protected virtual void OnPropertyChanged<TProperty>(Expression<Func<TProperty>> property)
        {
            if (property.Body is MemberExpression memberExpression)
                OnPropertyChanged(memberExpression.Member.Name);
        }
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}