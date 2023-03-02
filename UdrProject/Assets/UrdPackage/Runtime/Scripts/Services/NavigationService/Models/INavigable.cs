using System;

namespace Urd.Services.Navigation
{
    public interface INavigable
    {
        string Id { get; }
        NavigableStatus Status { get; }
        event Action<NavigableStatus, NavigableStatus> OnStatusChanged;

        public bool IsClosingOrDestroyed { get; }

        void ChangeStatus(NavigableStatus newNavigableStatus);
    }
}