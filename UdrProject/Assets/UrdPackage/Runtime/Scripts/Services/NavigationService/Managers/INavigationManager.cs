using System;

namespace Urd.Services.Navigation
{
    public interface INavigationManager
    {
        void Init();
        bool CanHandle(INavigable navigable);
        void Open(INavigable navigable, Action<bool> onOpenNavigable);
        bool CanOpen(INavigable navigable);
        void Close(INavigable navigable, Action<bool> onCloseNavigable);
    }
}