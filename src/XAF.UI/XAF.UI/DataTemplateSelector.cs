using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace XAF.UI
{
    public class DataTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        private readonly IList<IViewModelDescriptor> _descriptors;

        public DataTemplateSelector(IList<IViewModelDescriptor> descriptors)
        {
            _descriptors = descriptors;
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var viewModel = item as IViewModel;
            var descriptor = _descriptors?.FirstOrDefault(desc => (viewModel?.GetType().GetInterfaces().Any(i => i == desc.ViewModel)).GetValueOrDefault());

            if (descriptor == null || viewModel == null) return null;

            if (Device.Idiom == TargetIdiom.Phone) return CreateTemplate(viewModel.GetType(), descriptor.MobileView);

            return CreateTemplate(viewModel.GetType(), descriptor.TabletView);
        }

        private DataTemplate CreateTemplate(Type viewModelType, Type viewType)
        {
            if (viewModelType == null
                || viewType == null)
                return null;

            var xaml =
                $"<DataTemplate DataType=\"{{x:Type vm:{viewModelType.Name}}}\"><v:{viewType.Name} /></DataTemplate>";

            return new DataTemplate(() =>
            {
                var content = new ContentView().LoadFromXaml(xaml);
                return new ContentPage
                {
                    Content = content
                };
            });
        }
    }
}