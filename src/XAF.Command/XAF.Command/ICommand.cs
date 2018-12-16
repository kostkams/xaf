namespace XAF.Command
{
    public interface ICommand
    {
        void Run();
    }

    public interface ICommand<T>
    {
        T Run();
    }
}