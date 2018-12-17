using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Module = Autofac.Module;

namespace XAF.Autofac
{
    public class AutofacBuilder
    {
        private static readonly IDictionary<string, Assembly> assemblyCache;

        static AutofacBuilder()
        {
            assemblyCache = new ConcurrentDictionary<string, Assembly>();
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;
        }

        private static ModuleSearcher ModuleSearcher { get; set; }

        public void Build(ModuleSearcher moduleSearcher)
        {
            ModuleSearcher = moduleSearcher;

            var containerBuilder = new ContainerBuilder();

            var moduleFiles = (from directorySearch in ModuleSearcher.Directories
                               from file in
                                   directorySearch.Directory.EnumerateFiles("*.dll",
                                       directorySearch.Recursive
                                           ? SearchOption.AllDirectories
                                           : SearchOption.TopDirectoryOnly)
                               where Path.GetFileName(file.Name).EndsWith(".Module.dll")
                               select file).ToList();

            var modules = (from file in moduleFiles
                           let assembly = Assembly.LoadFile(file.FullName)
                           from type in assembly.GetTypes()
                           where type.IsClass && type.IsSubclassOf(typeof(Module))
                           select (IModule)Activator.CreateInstance(type)).ToList();

            modules.ForEach(module => containerBuilder.RegisterModule(module));

            var container = containerBuilder.Build();

            ServiceLocator.SetUp(container);
        }

        private static Assembly CurrentDomainOnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (ModuleSearcher == null)
                return null;
            
            var assemblyFileName = args.Name.Contains(',') ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name;
            var assembly = GetAssembly(assemblyFileName);

            return assembly;
        }

        private static Assembly GetAssembly(string assemblyFileName)
        {
            if (assemblyCache.ContainsKey(assemblyFileName))
                return assemblyCache[assemblyFileName];

            var assemblyFile = (from directorySearch in ModuleSearcher.Directories
                                from file in
                                    directorySearch.Directory.EnumerateFiles("*.dll",
                                        directorySearch.Recursive
                                            ? SearchOption.AllDirectories
                                            : SearchOption.TopDirectoryOnly)
                                where string.Equals(Path.GetFileNameWithoutExtension(file.Name), assemblyFileName)
                                select file).FirstOrDefault();

            if (assemblyFile == null)
                return null;

            var assembly = Assembly.LoadFrom(assemblyFile.FullName);
            assemblyCache.Add(assemblyFileName, assembly);

            return assembly;
        }
    }
}