using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XAF.UI
{
    internal static class XamlReader
    {
        private static readonly Func<BindableObject, string, BindableObject> LoadXamlFunc;

        static XamlReader()
        {
            var loadFromXamlMethodInfo = typeof(Extensions).GetTypeInfo()
                                                           .GetDeclaredMethods("LoadFromXaml")
                                                           .First(mi => mi.IsGenericMethod && mi.GetParameters().LastOrDefault()?.ParameterType == typeof(string));

            if (loadFromXamlMethodInfo == null)
            {
                LoadXamlFunc = (view, xaml) => throw new NotSupportedException("Xamarin.Forms implementation of XAML loading not found. Please update the Dynamic nuget package.");
            }
            else
            {
                loadFromXamlMethodInfo = loadFromXamlMethodInfo.MakeGenericMethod(typeof(BindableObject));
                LoadXamlFunc = (view, xaml) => (BindableObject) loadFromXamlMethodInfo.Invoke(null, new object[] {view, xaml});
            }
        }

        /// <summary>
        ///     Applies the given XAML to the view.
        /// </summary>
        public static TView LoadFromXaml<TView>(this TView view, string xaml) where TView : BindableObject
        {
            return (TView) LoadXamlFunc(view, xaml);
        }

        /// <summary>
        ///     Applies the given XAML to the view.
        /// </summary>
        public static TView LoadFromXaml<TView>(string xaml) where TView : BindableObject
        {
            var theView = (TView) Activator.CreateInstance(typeof(TView));
            return LoadFromXaml(theView, xaml);
        }
    }
}