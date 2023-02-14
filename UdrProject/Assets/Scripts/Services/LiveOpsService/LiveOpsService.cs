using System;
using System.Collections.Generic;
using Urd.LiveOps;

namespace Urd.Services
{
    public class LiveOpsService : BaseService, ILiveOpsService
    {
        private ILiveOpsProvider _liveOpsProvider;
        
        public void SetProvider(ILiveOpsProvider liveOpsProvider)
        {
            _liveOpsProvider = liveOpsProvider;
        }

        public void GetLiveOps<T>(LiveOpsTriggers trigger, Action<bool, List<T>> listElementsCallback)
            where T : IDeserializable
        {
            _liveOpsProvider.GetAllLiveOpsRaw(trigger, 
                                              listRawLiveOps => OnAllLiveOpsGet<T>(listRawLiveOps, listElementsCallback));
        }

        private void OnAllLiveOpsGet<T>(List<string> listRawLiveOps, Action<bool,List<T>> listElementsCallback) where T : IDeserializable
        {
            
        }
    }
}