using System;

namespace XAF.UI
{
    public interface IViewCommand
    {
        void Run(Action action);
    }

    public interface IViewCommand<T>
    {
        void Run(Action<T> action);
    }
}