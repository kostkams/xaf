using System;

namespace XAF.UI
{
    public class ViewModelDescriptor<TModel, TMobileView, TTabletView> : IViewModelDescriptor
        where TModel : IViewModel
    {
        public ViewModelDescriptor()
        {
            ViewModel = typeof(TModel);
            MobileView = typeof(TMobileView);
            TabletView = typeof(TTabletView);
        }

        public Type ViewModel { get; }

        public Type MobileView { get; }


        public Type TabletView { get; }
    }
}