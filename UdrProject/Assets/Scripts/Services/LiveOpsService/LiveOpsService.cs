using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Urd.LiveOps;

namespace Urd.Services
{
    public class LiveOpsService : BaseService, ILiveOpsService
    {
        private ILiveOpsProvider _liveOpsProvider;

        public override void Init()
        {
            base.Init();
            
            SetDefaultProvider();
            SetAsLoaded();
        }

        private void SetDefaultProvider()
        {
            SetProvider(new LiveOpsProviderForRemoteConfig());
        }

        public void SetProvider(ILiveOpsProvider liveOpsProvider)
        {
            _liveOpsProvider = liveOpsProvider;
        }

        public void GetLiveOps<T>(LiveOpsTriggers trigger, OnGetLiveOpsDelegate<T> listElementsCallback) where T : IDeserializable
        {
            _liveOpsProvider.GetAllLiveOpsRaw(trigger,
                                              listRawLiveOps =>
                                                  OnAllLiveOpsGet<T>(listRawLiveOps, listElementsCallback));
        }
        
        private void OnAllLiveOpsGet<T>(List<string> listRawLiveOps, OnGetLiveOpsDelegate<T> listElementsCallback)
            where T : IDeserializable
        {
            bool success = false;
            List<T> resultElements = new List<T>();
            
            for (int i = 0; i < listRawLiveOps.Count; i++)
            {
                try
                {
                    var newElement = JsonConvert.DeserializeObject<T>(listRawLiveOps[i]);
                    if (newElement.WasDeserializableSuccess)
                    {
                        success = true;
                        resultElements.Add(newElement);
                    }
                }
                catch
                {
                    continue;
                }
            }
            
            listElementsCallback?.Invoke(success, resultElements);
        }
    }
}