using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XAF.Autofac;

namespace XAF.UI
{
    public class DataTemplateSelector 
    {
        private readonly IList<IViewModelDescriptor> _descriptors;

        public DataTemplateSelector(IList<IViewModelDescriptor> descriptors)
        {
            _descriptors = descriptors;
        }

        public View CreateView(IViewModel viewModel)
        {
            var descriptor = _descriptors?.FirstOrDefault(desc => (viewModel?.GetType().GetInterfaces().Any(i => i == desc.ViewModel)).GetValueOrDefault());

            if (descriptor == null || viewModel == null) return null;

            if (Device.Idiom == TargetIdiom.Phone) return InternalCreateView(viewModel, descriptor.MobileView);

            return InternalCreateView(viewModel, descriptor.TabletView);
        }

        private View InternalCreateView(IViewModel viewModel, Type viewType)
        {
            if (viewModel == null || viewType == null)
                return null;

            var contentView = (ContentView)ServiceLocator.Current.Resolve(viewType);
            contentView.BindingContext = viewModel;
            return contentView;
        }
    }
}