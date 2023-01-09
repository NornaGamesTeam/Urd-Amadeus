using System;

namespace Urd.Services.Navigation
{
    public class NavigationPopupManager : INavigationManager
    {
        public NavigationPopupManager()
        {
            
        }

        public bool CanHandle(INavigable navigable)
        {
            return navigable is PopupModel;
        }

        public void Open(INavigable navigable, Action<bool> onOpenNavigable)
        {
            onOpenNavigable?.Invoke(true);
        }

        public bool CanOpen(INavigable navigable)
        {
            return true;
        }

        public void Close(INavigable navigable, Action<bool> onCloseNavigable)
        {
            onCloseNavigable?.Invoke(true);
        }
    }
}