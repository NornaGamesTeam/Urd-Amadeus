using System;
using Urd.Services.Navigation;

namespace Urd.Services
{
    public interface INavigationService : IBaseService
    {
        void Open(INavigable navigable, Action<bool> onOpenNavigableCallback);
        bool IsOpen(INavigable navigable);
        void Close(INavigable navigable, Action<bool> onCloseNavigable);

    }
}