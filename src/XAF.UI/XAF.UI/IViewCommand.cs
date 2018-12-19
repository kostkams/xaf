using System.Threading.Tasks;

namespace XAF.UI
{
    public interface IViewCommand
    {
        Task Run();
        Task Run(bool withBusy);
    }

    public interface IViewCommand<T>
    {
        Task<T> Run();

        Task<T> Run(bool withBusy);
    }
}