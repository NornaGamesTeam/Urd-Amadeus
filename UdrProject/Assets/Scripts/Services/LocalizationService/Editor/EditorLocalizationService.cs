
using System;
using System.Collections.Generic;
using UnityEditor;
using Urd.Services;
using Urd.Services.Localization;

namespace Urd.Editor
{
    public class EditorLocalizationService
    {
        private const string LOCALIZATION_CONFIG_FILE_PATH = "LocalizationConfig";

        [MenuItem("Urd/Localization/SelectConfigFile")]
        public static void OpenConfigFile() => UrdEditorUtils.GetConfigFile(LOCALIZATION_CONFIG_FILE_PATH);

        private static ResourceHelper<LocalizationConfig> _resourceHelper = new ResourceHelper<LocalizationConfig>(LOCALIZATION_CONFIG_FILE_PATH);

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