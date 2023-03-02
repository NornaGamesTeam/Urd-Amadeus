using System;
using System.Collections.Generic;
using UnityEngine;

namespace Urd.Services.Localization
{
    public class LocalizationConfig : ScriptableObject
    {
        private const string LOCALIZATION_FOLDER_PATH = "Localization";

        [field: SerializeField]
        public LocalizationLanguages EditorLanguage { get; set; }

        [SerializeField]
        List<LocalizationConfigInfo> _languageCache = new List<LocalizationConfigInfo>();

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

        public void SetFileForLanguage(LocalizationLanguages language)
        {
            var textAsset = Resources.Load<TextAsset>(LOCALIZATION_FOLDER_PATH + "/" + language + ".json");
            if(textAsset == null)
            {
                Debug.LogWarning($"[LocalizationConfig] cannot assign file for language {language}");
                return;
            }
            var languageConfig = _languageCache.Find(languageConfigData => languageConfigData.Language == language);
            languageConfig.SetFile(textAsset);
        }
    }

    [System.Serializable]
    public class LocalizationConfigInfo
    {
        [field: SerializeField]
        public LocalizationLanguages Language { get; private set; }
        [field: SerializeField]
        public TextAsset File { get; private set; }

        public void SetFile(TextAsset textAsset)
        {
            File = textAsset;
        }
    }
}