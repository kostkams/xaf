namespace XAF.UI
{
    public interface IItemViewCommand : IViewCommand
    {
        string Id { set; }
    }

    public interface IItemViewCommand<T> : IViewCommand<T>
    {
        string Id { set; }
    }
}