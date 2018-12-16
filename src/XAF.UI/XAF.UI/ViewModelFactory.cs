using System;
using System.Collections.Generic;
using Autofac;
using XAF.Autofac;

namespace XAF.UI
{
    public class ViewModelFactory : IViewModelFactory
    {
        private static readonly Dictionary<Type, ILifetimeScope> LifetimeScopeLookups =
            new Dictionary<Type, ILifetimeScope>();

        public IViewModel Create<T>() where T : IViewModel
        {
            ILifetimeScope lifetime;
            if (LifetimeScopeLookups.ContainsKey(typeof(T)))
                lifetime = LifetimeScopeLookups[typeof(T)];
            else
                lifetime = ServiceLocator.Current.BeginLifetimeScope();
            var viewModel = (IViewModel) lifetime.Resolve<T>();


            if (!LifetimeScopeLookups.ContainsKey(typeof(T)))
                LifetimeScopeLookups.Add(typeof(T), lifetime);

            return viewModel;
        }

        public void Release(IViewModel viewModelInstance)
        {
            var lifetimeScope = LifetimeScopeLookups[viewModelInstance.GetType()];
            LifetimeScopeLookups.Remove(viewModelInstance.GetType());
            lifetimeScope.Dispose();
        }

        public IViewModel Create(Type viewModelType)
        {
            ILifetimeScope lifetime;
            if (LifetimeScopeLookups.ContainsKey(viewModelType))
                lifetime = LifetimeScopeLookups[viewModelType];
            else
                lifetime = ServiceLocator.Current.BeginLifetimeScope();

            var viewModel = (IViewModel) lifetime.Resolve(viewModelType);

            if (!LifetimeScopeLookups.ContainsKey(viewModelType))
                LifetimeScopeLookups.Add(viewModelType, lifetime);

            return viewModel;
        }
    }
}