using System;
using System.Collections.Generic;
using UnityEngine;

namespace Urd.Services.Localization
{
    public class LocalizationConfig : ScriptableObject
    {
        [SerializeField]
        List<LocalizationConfigInfo> _languageCache = new List<LocalizationConfigInfo>();

        [field: SerializeField]
        public LocalizationLanguages EditorLanguage { get; set; }

        public Dictionary<string, string> GetLanguageForLanguage(LocalizationLanguages language)
        {
            var languageConfig = _languageCache.Find(languageConfig => languageConfig.Language == language) ??
                _languageCache.Find(languageConfig => languageConfig.Language == LocalizationLanguages.English);

            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(languageConfig.File.text);
            }
            catch(Exception exception)
            {
                Debug.LogWarning($"[LocalizationConfig] Error when try to get the language {language}. Error:{exception}");
                return new Dictionary<string, string>();
            }
        }
    }

    [System.Serializable]
    public class LocalizationConfigInfo
    {
        [field: SerializeField]
        public LocalizationLanguages Language { get; private set; }
        [field: SerializeField]
        public TextAsset File { get; private set; }
    }
}