using System;
using System.Collections.Generic;
using Urd.LiveOps;

namespace Urd.Services
{
    public delegate void OnGetLiveOpsDelegate<T>(bool success, List<T> elementList);
    
    public interface ILiveOpsService : IBaseService
    {
        void SetProvider(ILiveOpsProvider liveOpsProvider);

        void GetLiveOps<T>(LiveOpsTriggers trigger, OnGetLiveOpsDelegate<T> listElementsCallback) where T : IDeserializable;
    }
}