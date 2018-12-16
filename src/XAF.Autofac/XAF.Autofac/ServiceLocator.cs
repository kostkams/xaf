using Autofac;

namespace XAF.Autofac
{
    public class ServiceLocator
    {
        public static ILifetimeScope Current { get; private set; }

        internal static void SetUp(IContainer container)
        {
            Current = container.BeginLifetimeScope();
        }
    }
}