using System;
using System.Collections.Generic;
using Urd.LiveOps;

namespace Urd.Services
{
    public interface ILiveOpsService : IBaseService
    {
        void SetProvider(ILiveOpsProvider liveOpsProvider);

        void GetLiveOps<T>(LiveOpsTriggers trigger, Action<bool, List<T>> listElementsCallback) where T : IDeserializable;
    }
}