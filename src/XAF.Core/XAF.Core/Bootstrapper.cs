using System.Collections.Generic;
using XAF.Autofac;

namespace XAF.Core
{
    public class Bootstrapper
    {
        public void Startup(ModuleSearcher moduleSearcher)
        {
            new AutofacBuilder().Build(moduleSearcher);
        }
    }
}