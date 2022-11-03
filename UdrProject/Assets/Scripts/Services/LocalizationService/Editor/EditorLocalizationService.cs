
using System;
using System.Collections.Generic;
using System.IO;
using Unity.Services.RemoteConfig;
using UnityEditor;
using UnityEngine;
using Urd.Services;
using Urd.Services.Localization;

namespace Urd.Editor
{
    public class EditorLocalizationService
    {
        private const string LOCALIZATION_CONFIG_FILE_PATH = "LocalizationConfig";
        private const string LOCALIZATION_FOLDER_PATH = "Localization";

        private static ResourceHelper<LocalizationConfig> _resourceHelper = new ResourceHelper<LocalizationConfig>(LOCALIZATION_CONFIG_FILE_PATH);

        private static IEditorLocalizationServiceProvider _editorLocalizationServiceProvider = new EditorRemoteConfigLocalizationServiceProvider();

        [MenuItem("Urd/Localization/SelectConfigFile")]
        public static void OpenConfigFile() => UrdEditorUtils.GetConfigFile(LOCALIZATION_CONFIG_FILE_PATH);

        [MenuItem("Urd/Localization/UpdateLocalization")]
        public static void UpdateLocalizationFiles()
        {
            for (LocalizationLanguages language = LocalizationLanguages.None+1; language < LocalizationLanguages.Size; language++)
            {
                var languageToLoad = language;
                _editorLocalizationServiceProvider.FetchLocalization(languageToLoad, (dictionary) => OnLocalizationFetched(languageToLoad, dictionary));
            }
        }

        private static void OnLocalizationFetched(LocalizationLanguages language, Dictionary<string, string> dictionary)
        {
            if(dictionary.Count <= 0)
            {
                UnityEngine.Debug.LogWarning($"[EditorLocalizationService] no dictionary loaded for language: {language}");
                return;
            }

            SaveFile(language, dictionary);
            _resourceHelper.FileLoaded.SetFileForLanguage(language);
        }

        private static void SaveFile(LocalizationLanguages language, Dictionary<string, string> dictionary)
        {
            string folderPath = Application.persistentDataPath + "/" + LOCALIZATION_FOLDER_PATH;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = folderPath + language.ToString() + ".json";
            File.WriteAllText(filePath, Newtonsoft.Json.JsonConvert.SerializeObject(dictionary).ToString());
        }

        public static string Locate(string key)
        {
            var localizationConfig = _resourceHelper.FileLoaded;
            var localizationKeys = localizationConfig.GetLanguageForLanguage(localizationConfig.EditorLanguage);

            if(localizationKeys.TryGetValue(key, out var value))
            {
                return value;
            }

            return key;
        }


    }
}