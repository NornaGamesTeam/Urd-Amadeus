using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Urd.Utils;

namespace Urd.Services.Localization
{
    public class RemoteConfigurationLocalizationKeyProvider : ILocalizationKeysProvider
    {
        private const string LOCALIZATION_REMOTE_CONFIG_KEY = "LocalizationKeyValues";

        private ServiceHelper<IRemoteConfigurationService> _remoteConfiguration =
            new ServiceHelper<IRemoteConfigurationService>();
        
        public RemoteConfigurationLocalizationKeyProvider()
        {
            
        }

        public void GetLocalization(LocalizationLanguages language, Action<Dictionary<string, string>> localizationKeyValuesCallback)
        {
            if (_remoteConfiguration.HasService)
            {
                _remoteConfiguration.OnInitialize += () => GetLocalization(language, localizationKeyValuesCallback);
                return;
            }
            
            if (!_remoteConfiguration.Service.TryGetDataAs<Dictionary<string,string>>(LOCALIZATION_REMOTE_CONFIG_KEY+language.ToString(), out var keyValuePairs))
            {
                keyValuePairs = new Dictionary<string, string>();
            }

            localizationKeyValuesCallback?.Invoke(keyValuePairs);
        }        
    }
}