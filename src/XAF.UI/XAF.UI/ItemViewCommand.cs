namespace XAF.UI
{
    public abstract class ItemViewCommand : ViewCommand, IItemViewCommand
    {
        public string Id { get; set; }
    }

    public abstract class ItemViewCommand<T> : ViewCommand<T>, IItemViewCommand<T>
    {
        public string Id { get; set; }
    }
}