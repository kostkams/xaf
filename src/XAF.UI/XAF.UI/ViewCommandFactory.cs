using Autofac;
using XAF.Autofac;

namespace XAF.UI
{
    public class ViewCommandFactory : IViewCommandFactory
    {
        public T Resolve<T>() where T : IViewCommand
        {
            return ServiceLocator.Current.Resolve<T>();
        }

        public T Resolve<T, V>() where T : IViewCommand<V>
        {
            return ServiceLocator.Current.Resolve<T>();
        }
    }
}