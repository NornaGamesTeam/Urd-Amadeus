using System;

namespace Urd.Services.Navigation
{
    public abstract class Navigable : INavigable
    {
        public abstract string Id { get; }
        public NavigableStatus Status { get; private set; }
        public Action<NavigableStatus, NavigableStatus> OnStatusChanged { get; }

        public void ChangeStatus(NavigableStatus newNavigableStatus)
        {
            if(Status != newNavigableStatus)
            {
                var lastStatus = Status;
                Status = newNavigableStatus;
                OnStatusChanged?.Invoke(lastStatus, lastStatus);
            }
        }
    }
}