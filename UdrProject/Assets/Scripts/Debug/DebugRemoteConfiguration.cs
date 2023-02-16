using UnityEngine;
using Urd.Scene;
using Urd.Services;
using Urd.Services.RemoteConfiguration;
using Urd.Utils;

namespace Urd.DebugTools
{
    public class DebugRemoteConfiguration : DebugAbstract
    {
        [SerializeField] 
        private string _remoteConfigToOpen;
        
        private IRemoteConfigurationService _remoteConfig;
        public override void OnInputGetDown()
        {
            _remoteConfig = StaticServiceLocator.Get<IRemoteConfigurationService>();
            _remoteConfig.SetEnvironment(RemoteConfigurationEnvironmentType.test);
            _remoteConfig.FetchData(OnDataFetched);
        }

        private void OnDataFetched()
        {
            _remoteConfig.TryGetDataAs(_remoteConfigToOpen, out string data);
            Debug.Log($"[DebugRemoteConfiguration]: {data}");
        }
    }
}