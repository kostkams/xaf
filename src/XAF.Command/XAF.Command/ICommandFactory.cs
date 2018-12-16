namespace XAF.Command
{
    public interface ICommandFactory
    {
        T Resolve<T>() where T : ICommand;
        T Resolve<T, V>() where T : ICommand<V>;
    }
}