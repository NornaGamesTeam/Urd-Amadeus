using System;
using System.Collections.Generic;
using Urd.Services.Navigation;

namespace Urd.Services
{
    public class NavigationService : BaseService, INavigationService
    {
        private List<INavigable> _navigableOpened = new List<INavigable>();
        private List<INavigable> _navigableHistory = new List<INavigable>();

        public override void Init()
        {
            base.Init();
        }

        public void Open(INavigable navegable, Action<bool> OnOpenNavegable)
        {
            _navigableOpened.Add(navegable);
            navegable.ChangeStatus(NavigableStatus.Open);

            AddToHistory(navegable);

            OnOpenNavegable?.Invoke(true);

            navegable.ChangeStatus(NavigableStatus.Idle);
        }

        public bool IsOpen(INavigable navegable)
        {
            return _navigableOpened.Exists(navegableOpened => navegableOpened.Id == navegable.Id);
        }

        private void AddToHistory(INavigable navegable)
        {
            _navigableHistory.Add(navegable);
        }

        public void Close(INavigable navegable, Action<bool> OnCloseNavegable)
        {
            _navigableOpened.Remove(navegable);
            navegable.ChangeStatus(NavigableStatus.Closed);

            OnCloseNavegable?.Invoke(true);
        }
    }
}