using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using Urd.Services.RemoteConfiguration;

namespace Urd.Services
{
    public class RemoteConfigurationService : BaseService, IRemoteConfigurationService
    {
        private IRemoteConfigurationProvider _provider;

        private Dictionary<string, string> _keyValues = new();

        public RemoteConfigurationEnvironmentType Environment { get; private set; } =
            RemoteConfigurationEnvironmentType.production;
        
        public override void Init()
        {
            base.Init();

            SetProvider(new RemoteConfigurationProviderUnity());
            UnityServices.InitializeAsync();
        }
        
        public void SetProvider(IRemoteConfigurationProvider newProvider)
        {
            _provider = newProvider;
            _provider.OnGetRemoteConfigurationData += OnGetRemoteConfigurationData;
            FetchData(null);
        }
        
        public void SetEnvironment(RemoteConfigurationEnvironmentType environmentType)
        {
            Environment = environmentType;
            _provider.SetEnvironment(Environment);
            FetchData(null);
        }

        public void FetchData(Action onFetchDataCallback)
        {
            _provider.FetchData(() => OnFetchData(onFetchDataCallback));
        }

        private void OnFetchData(Action onFetchDataCallback)
        {
            SetAsLoaded();
            onFetchDataCallback?.Invoke();
        }

        private void OnGetRemoteConfigurationData(Dictionary<string, string> keyValues)
        {
            _keyValues = keyValues;
        }

        public bool TryGetDataAs(string key, out string value) => _keyValues.TryGetValue(key, out value);

        public bool TryGetDataAs<T>(string key, out T value)
        {
            value = default(T);

            if(!_keyValues.TryGetValue(key, out string rawValue))
            {
                UnityEngine.Debug.LogWarning($"[RemoteConfigurationService] Doesn't contains the key {key}");
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