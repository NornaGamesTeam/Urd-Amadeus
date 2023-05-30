using System;
using Urd.Services.Navigation;

namespace Urd.Services
{
    public interface INavigationService : IBaseService
    {
        public event Action<INavigable> OnFinishLoadNavigable;

        public TNavigable GetModel<TEnum, TNavigable>(TEnum enumValue)
            where TEnum : Enum where TNavigable : class, INavigable;
        public void Open<TEnum, TNavigable>(TEnum enumValue, Action<bool> onOpenNavigableCallback)
            where TEnum : Enum where TNavigable : class, INavigable;
        void Open(INavigable navigable, Action<bool> onOpenNavigableCallback = null);
        bool IsOpen(INavigable navigable);
        void Close(INavigable navigable, Action<bool> onCloseNavigableCallback = null);

    }
}