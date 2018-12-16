using Autofac;
using XAF.UI;

namespace XAF.Core.Module
{
    public class ViewModelModule : global::Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register<IViewModelFactory>(c => new ViewModelFactory())
                   .SingleInstance();
        }
    }
}