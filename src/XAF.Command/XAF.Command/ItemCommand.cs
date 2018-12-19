namespace XAF.Command
{
    public abstract class ItemCommand : IItemCommand
    {
        public string Id { get; set; }
        public abstract void Run();
    }

    public abstract class ItemCommand<T> : IItemCommand<T>
    {
        public string Id { get; set; }
        public abstract T Run();
    }
}