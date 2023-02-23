using System;
using System.Collections.Generic;
using Unity.Services.RemoteConfig;
using UnityEngine;

namespace Urd.Services.RemoteConfiguration
{
    public class RemoteConfigurationProviderUnity : IRemoteConfigurationProvider
    {
        private string REMOTE_CONFIGURATION_PATH = "RemoteConfiguration";
        private string REMOTE_CONFIGURATION_CONFIG = "RemoteConfiguration";

        private RemoteConfigurationEnvironmentsConfig _remoteConfigurationConfig;

        public bool IsFetching { get; private set; }
        
        private event Action _onFetchDataCallback;

        private struct DummyStruct
        {

        }

        public event IRemoteConfigurationProvider.OnGetRemoteConfigurationDataDelegate OnGetRemoteConfigurationData;

        public RemoteConfigurationProviderUnity()
        {
            RemoteConfigService.Instance.FetchCompleted += OnFetchCompleted;
            LoadResourceData();
        }

        void LoadResourceData()
        {
            var fullPath = REMOTE_CONFIGURATION_PATH + "/" +
                           REMOTE_CONFIGURATION_CONFIG; 
            _remoteConfigurationConfig = Resources.Load<RemoteConfigurationEnvironmentsConfig>(fullPath);

            if (_remoteConfigurationConfig == null)
            {
                Debug.LogWarning($"[RemoteConfigurationProviderUnity] Error when try to get the config in path: {fullPath}");
            }
        }

        public void FetchData(Action onFetchData)
        {
            _onFetchDataCallback += onFetchData;
            if (!IsFetching)
            {
                IsFetching = true;
                RemoteConfigService.Instance.FetchConfigs(new DummyStruct(), new DummyStruct());
            }
        }

        public void SetEnvironment(RemoteConfigurationEnvironmentType environmentType)
        {
            string environmentId = string.Empty;
            if (_remoteConfigurationConfig?.TryGetEnvironment(environmentType, out environmentId) == true)
            {
                RemoteConfigService.Instance.SetEnvironmentID(environmentId);
            }
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

            OnGetRemoteConfigurationData?.Invoke(newKeys);
            _onFetchDataCallback?.Invoke();
            _onFetchDataCallback = null;
            IsFetching = false;
        }
    }
}