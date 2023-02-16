using System;
using System.Collections.Generic;
using Urd.Services;
using Urd.Utils;

namespace Urd.LiveOps
{
    public class LiveOpsProviderForRemoteConfig: ILiveOpsProvider
    {
        private IRemoteConfigurationService _remoteConfigService;
        public LiveOpsProviderForRemoteConfig()
        {
            _remoteConfigService = StaticServiceLocator.Get<IRemoteConfigurationService>();
        }
        public void GetAllLiveOpsRaw(LiveOpsTriggers trigger, Action<List<string>> callback)
        {
            _remoteConfigService.TryGetDataAs(trigger.ToString(), out var rawData);
            callback?.Invoke(new List<string>(){ rawData });
        }
    }
}