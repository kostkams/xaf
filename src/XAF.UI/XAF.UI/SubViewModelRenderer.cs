using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace XAF.UI
{
    public class MainSubViewModelRenderer : ContentView
    {
        private IMainViewModel oldViewModel;

        public MainSubViewModelRenderer()
        {
            BindingContextChanged += OnBindingContextChanged;
            UpdateContent();
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            UpdateContent();
        }

        private void UpdateContent()
        {
            var viewModel = BindingContext as IMainViewModel;

            if (viewModel?.SubViewModel == null)
                return;

            viewModel.PropertyChanged += ViewModelOnPropertyChanged;
            UpdateContent(viewModel);
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "SubViewModel")
                return;

            var viewModel = BindingContext as IMainViewModel;

            if (viewModel?.SubViewModel == null)
                viewModel = oldViewModel;
            if (viewModel?.SubViewModel == null)
                return;

            UpdateContent(viewModel);
        }

        private void UpdateContent(IMainViewModel viewModel)
        {
            oldViewModel = viewModel;
            BindingContext = viewModel.SubViewModel;
            Content = ((MainViewModel) viewModel).DataTemplateSelector.CreateView(viewModel.SubViewModel);
        }
    }
}