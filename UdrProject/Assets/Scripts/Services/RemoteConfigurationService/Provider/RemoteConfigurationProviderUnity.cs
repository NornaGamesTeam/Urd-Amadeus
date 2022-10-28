using System;
using System.Collections.Generic;
using Unity.Services.RemoteConfig;

namespace Urd.Services.RemoteConfiguration
{
    public class RemoteConfigurationProviderUnity : IRemoteConfigurationProvider
    {
        private Action _onFetchDataCallback;

        private struct DummyStruct
        {

        }

        public event IRemoteConfigurationProvider.OnGetRemoteConfigurationDataDelegate OnGetRemoteConfigurationData;

        public RemoteConfigurationProviderUnity()
        {
            RemoteConfigService.Instance.FetchCompleted += OnFetchCompleted;
        }

        public void FetchData(Action onFetchData)
        {
            RemoteConfigService.Instance.FetchConfigs(new DummyStruct(), new DummyStruct());
            _onFetchDataCallback = onFetchData;
        }

        private void OnFetchCompleted(ConfigResponse configResponse)
        {
            Dictionary<string, string> newKeys = new Dictionary<string, string>();
            if (configResponse.status == ConfigRequestStatus.Success)
            {
                foreach (var config in RemoteConfigService.Instance.appConfig.config)
                {
                    if(config.Value.Type != Newtonsoft.Json.Linq.JTokenType.Boolean)
                    {
                        newKeys[config.Key] = config.Value.ToString();
                    }
                    else
                    {
                        newKeys[config.Key] = config.Value.ToString().ToLower();
                    }
                }
            }

            _onFetchDataCallback?.Invoke();
            _onFetchDataCallback = null;
            OnGetRemoteConfigurationData?.Invoke(newKeys);
        }
    }
}