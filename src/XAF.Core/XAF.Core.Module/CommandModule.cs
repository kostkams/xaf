using Autofac;
using XAF.Command;

namespace XAF.Core.Module
{
    public class CommandModule : global::Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register<ICommandFactory>(c => new CommandFactory())
                   .SingleInstance();
        }
    }
}