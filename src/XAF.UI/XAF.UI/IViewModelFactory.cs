﻿using System;

namespace XAF.UI
{
    public interface IViewModelFactory
    {
        T Create<T>() where T : IViewModel;

        IViewModel Create(Type viewModelType);

        void Release(IViewModel viewModelInstance);
    }
}