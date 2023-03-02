using System;
using System.Collections.Generic;

namespace Urd.LiveOps
{
    public interface ILiveOpsProvider
    {
        void GetAllLiveOpsRaw(LiveOpsTriggers trigger, Action<List<string>> callback);
        void Init(Action onInitialize);
    }
}