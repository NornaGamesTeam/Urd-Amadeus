using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Urd.Services.Localization;
using Urd.Utils;

namespace Urd.UrdEditor
{
    public class EditorLocalizationService
    {
        private const string LOCALIZATION_CONFIG_FILE_PATH = "LocalizationConfig";
        private const string LOCALIZATION_FOLDER_PATH = "Localization";

        private static ResourceHelper<LocalizationConfig> _resourceHelper = 
            new ResourceHelper<LocalizationConfig>(LOCALIZATION_FOLDER_PATH+"/"+LOCALIZATION_CONFIG_FILE_PATH);

        private static IEditorLocalizationServiceProvider _editorLocalizationServiceProvider = new EditorRemoteConfigLocalizationServiceProvider();

        [MenuItem("Urd/Localization/SelectConfigFile", false, 100)]
        public static void OpenConfigFile() => UrdEditorUtils.GetConfigFile(LOCALIZATION_CONFIG_FILE_PATH);

        [MenuItem("Urd/Localization/UpdateLocalization", false, 100)]
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

        public static string Locate(string key, LocalizationLanguages language = LocalizationLanguages.None)
        {
            var localizationConfig = _resourceHelper.FileLoaded;
            language = language != LocalizationLanguages.None ? language : localizationConfig.EditorLanguage;
            var localizationKeys = localizationConfig.GetLanguageForLanguage(language);

            if(localizationKeys.TryGetValue(key, out var value))
            {
                return value;
            }

            return key;
        }

        public static Dictionary<string, string> GetKeysValues() => 
            _resourceHelper.FileLoaded?.GetLanguageForLanguage(LocalizationLanguages.English)
            ?? new Dictionary<string, string>();

    }
}