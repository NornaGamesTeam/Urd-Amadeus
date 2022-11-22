using System;
using Urd.Services.Navigation;

namespace Urd.Services
{
    public interface INavigationService : IBaseService
    {
        void Open(INavigable navegable, Action<bool> OnOpenNavegable);
        bool IsOpen(INavigable navegable);
        void Close(INavigable navegable, Action<bool> OnCloseNavegable);

    }
}