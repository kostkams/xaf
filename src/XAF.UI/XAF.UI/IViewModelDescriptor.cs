using System;

namespace XAF.UI
{
    public interface IViewModelDescriptor
    {
        Type ViewModel { get; }
        Type MobileView { get; }
        Type TabletView { get; }
    }
}