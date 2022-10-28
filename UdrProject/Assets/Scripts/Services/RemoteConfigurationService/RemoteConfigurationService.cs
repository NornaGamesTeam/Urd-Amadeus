using System;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Urd.Services.RemoteConfiguration;

namespace Urd.Services
{
    public class RemoteConfigurationService : BaseService, IRemoteConfigurationService
    {
        private IRemoteConfigurationProvider _provider;

        Dictionary<string, string> _keyValues = new Dictionary<string, string>();

        public override void Init()
        {
            base.Init();

            UnityServices.InitializeAsync();

            SetProvider(new RemoteConfigurationProviderUnity());

            //FetchData(null);
        }

        public void SetProvider(IRemoteConfigurationProvider newProvider)
        {
            _provider = newProvider;
            _provider.OnGetRemoteConfigurationData += OnGetRemoteConfigurationData;
        }

        public void FetchData(Action onFetchData)
        {
            _provider.FetchData(onFetchData);
        }

        private void OnGetRemoteConfigurationData(Dictionary<string, string> keyValues)
        {
            _keyValues = keyValues;
        }

        public bool TryGetDataAs<T>(string key, out T value)
        {
            value = default(T);
            if(!_keyValues.TryGetValue(key, out string rawValue))
            {
                UnityEngine.Debug.LogWarning($"[RemoteConfigurationService] Cannot contains the key {key}");
                return false;
            }

            try
            {
                value = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(rawValue);
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogWarning($"[RemoteConfigurationService] Cannot convert the rawData {value} as type: {typeof(T)}. Error:{exception}");
                return false;
            }

            return true;
        }
    }
}