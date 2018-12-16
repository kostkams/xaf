using Autofac;
using XAF.Autofac;

namespace XAF.Command
{
    public class CommandFactory : ICommandFactory
    {
        public T Resolve<T>() where T : ICommand
        {
            return ServiceLocator.Current.Resolve<T>();
        }

        public T Resolve<T, V>() where T : ICommand<V>
        {
            return ServiceLocator.Current.Resolve<T>();
        }
    }
}