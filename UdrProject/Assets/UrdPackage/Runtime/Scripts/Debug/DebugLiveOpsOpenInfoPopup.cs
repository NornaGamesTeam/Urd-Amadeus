using System.Collections.Generic;
using UnityEngine;
using Urd.LiveOps;
using Urd.Popup;
using Urd.Scene;
using Urd.Services;
using Urd.Services.RemoteConfiguration;
using Urd.Utils;

namespace Urd.DebugTools
{
    public class DebugLiveOpsOpenInfoPopup : DebugAbstract
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
            StaticServiceLocator.Get<ILiveOpsService>().
                                 GetLiveOps<PopupInfoModel>(LiveOpsTriggers.OnTestInfoPopup, OnGetLiveOps);
        }

        private void OnGetLiveOps(bool success, List<PopupInfoModel> elementList)
        {
            if (!success)
            {
                Debug.LogWarning($"OnInputGetDown: {success}, elementList: {elementList.ToJson()}");
                return;
            }
            
            StaticServiceLocator.Get<INavigationService>().Open(elementList[0]);
        }
    }
}