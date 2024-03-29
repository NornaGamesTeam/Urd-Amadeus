using System;
using System.Collections.Generic;
using Urd.Services;
using Urd.Utils;

namespace Urd.LiveOps
{
    [Serializable]
    public class LiveOpsProviderForRemoteConfig: ILiveOpsProvider
    {
        private ServiceHelper<IRemoteConfigurationService> _remoteConfigService =
            new ServiceHelper<IRemoteConfigurationService>();
        public LiveOpsProviderForRemoteConfig()
        {

        }
        public void Init(Action onInitialize)
        {
            onInitialize?.Invoke();
        }
        
        public void GetAllLiveOpsRaw(LiveOpsTriggers trigger, Action<List<string>> callback)
        {
            _remoteConfigService.Service.TryGetDataAs(trigger.ToString(), out var rawData);
            callback?.Invoke(new List<string>(){ rawData });
        }

        
    }
}