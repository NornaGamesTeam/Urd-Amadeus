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
            throw new NotImplementedException();
        }

        public bool CanOpen(INavigable navigable)
        {
            return true;
        }

        public bool IsOpen(INavigable navigable)
        {
            throw new NotImplementedException();
        }

        public void Close(INavigable navigable, Action<bool> onCloseNavigable)
        {
            throw new NotImplementedException();
        }
    }
}