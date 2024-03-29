using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Urd.Services.Navigation
{
    public abstract class Navigable : INavigable
    {
        public abstract string Id { get; }
        [JsonConverter(typeof(StringEnumConverter))]
        public NavigableStatus Status { get; private set; }
        public event Action<NavigableStatus, NavigableStatus> OnStatusChanged;
        
        public bool IsClosingOrDestroyed => Status == NavigableStatus.Closing ||
                                            Status == NavigableStatus.Closed ||
                                            Status == NavigableStatus.Destroyed;

        public void ChangeStatus(NavigableStatus newNavigableStatus)
        {
            if(Status != newNavigableStatus)
            {
                var lastStatus = Status;
                Status = newNavigableStatus;
                OnStatusChanged?.Invoke(lastStatus, Status);
            }
        }
    }
}