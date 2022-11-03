using System;
using System.Collections.Generic;
using Unity.Services.RemoteConfig;
using Urd.Services.Localization;

namespace Urd.Editor
{
    public class EditorRemoteConfigLocalizationServiceProvider : IEditorLocalizationServiceProvider
    {
        private const string LOCALIZATION_REMOTE_CONFIG_KEY = "LocalizationKeyValues";

        public void FetchLocalization(LocalizationLanguages language, Action<Dictionary<string, string>> onLocalizationFetched)
        {
            var jsonData = LoadJson(language);
            if (string.IsNullOrEmpty(jsonData))
            {
                RemoteConfigService.Instance.FetchCompleted += (onFetch) => OnFetchCompleted(language, onLocalizationFetched);
            }
            else
            {
                ParseData(jsonData, onLocalizationFetched);
            }
        }

        private string LoadJson(LocalizationLanguages language)
        {
            var jsonData = RemoteConfigService.Instance.appConfig.GetJson(LOCALIZATION_REMOTE_CONFIG_KEY + language, string.Empty);
            return jsonData;
        }

        private void ParseData(string jsonData, Action<Dictionary<string, string>> onLocalizationFetched)
        {
            try
            {
                var newLocalization = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
                onLocalizationFetched?.Invoke(newLocalization);
                onLocalizationFetched = null;
            }
            catch (Exception exception)
            {
                UnityEngine.Debug.LogWarning($"[EditorRemoteConfigProvider] Error when try to parse the jsonData: {jsonData} with error: {exception}");
                onLocalizationFetched?.Invoke(new Dictionary<string, string>());
                onLocalizationFetched = null;
            }
        }


        private void OnFetchCompleted(LocalizationLanguages language, Action<Dictionary<string, string>> onLocalizationFetched)
        {
            var jsonData = LoadJson(language);
            ParseData(jsonData, onLocalizationFetched);
        }
    }
}