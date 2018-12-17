using Autofac;
using XAF.RestClient;

namespace XAF.Core.Module
{
    public class RestClientModule : global::Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register<IRestClient>(c => new RestClient.RestClient())
                   .SingleInstance();
        }
    }
}