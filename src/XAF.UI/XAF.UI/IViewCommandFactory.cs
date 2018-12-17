namespace XAF.UI
{
    public interface IViewCommandFactory
    {
        T Resolve<T>() where T : IViewCommand;
        T Resolve<T, V>() where T : IViewCommand<V>;
    }
}