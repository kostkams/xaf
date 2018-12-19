namespace XAF.Command
{
    public interface IItemCommand : ICommand
    {
        string Id { set; }
    }

    public interface IItemCommand<T> : ICommand<T>
    {
        string Id { set; }
    }
}