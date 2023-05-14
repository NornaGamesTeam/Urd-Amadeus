using System;
using Urd.Services.Navigation;

namespace Urd.Services
{
    public interface INavigationService : IBaseService
    {
        public event Action<INavigable> OnFinishLoadNavigable;

        void Open(INavigable navigable, Action<bool> onOpenNavigableCallback = null);
        bool IsOpen(INavigable navigable);
        void Close(INavigable navigable, Action<bool> onCloseNavigableCallback = null);

    }
}