using System;

namespace Urd.Services.Navigation
{
    public interface INavigable
    {
        string Id { get; }
        NavigableStatus Status { get; }
        Action<NavigableStatus, NavigableStatus> OnStatusChanged { get; }

        void ChangeStatus(NavigableStatus newNavigableStatus);
    }
}