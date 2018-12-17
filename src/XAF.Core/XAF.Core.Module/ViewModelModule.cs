using System.Collections.Generic;
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

            builder.Register<IMainViewModel>(c => new MainViewModel())
                   .OnActivated(handler => handler.Instance.RegisterViewModelDescriptors(handler.Context.Resolve<IList<IViewModelDescriptor>>()))
                   .SingleInstance();
        }
    }
}