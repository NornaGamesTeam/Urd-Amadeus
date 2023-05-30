using System;

namespace Urd.Services.Navigation
{
    public interface INavigationManager : IProvider
    {
        bool IsInitialized { get; }
        INavigable GetModel<TEnum>(TEnum enumValue) where TEnum : Enum;
        bool CanHandle<TEnum>(TEnum enumValue) where TEnum : Enum;
        bool CanHandle(INavigable navigable);
        void Open(INavigable navigable, Action<bool> onOpenNavigable);
        bool CanOpen(INavigable navigable);
        void Close(INavigable navigable, Action<bool> onCloseNavigable);
    }
}