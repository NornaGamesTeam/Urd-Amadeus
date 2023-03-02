using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Urd.Services.Localization;
using Urd.UI;
using Urd.Utils;

namespace Urd.UrdEditor
{
    public class EditorLogoConfig
    {
        private const string LOGO_CONFIG_FILE_PATH = "LogoConfig";
        private const string LOGO_FOLDER_PATH = "";

        private static ResourceHelper<LogoConfig> _resourceHelper = 
            new ResourceHelper<LogoConfig>(LOGO_FOLDER_PATH+"/"+LOGO_CONFIG_FILE_PATH);

        [MenuItem("Urd/UI/LogoConfig", false, 1)]
        public static void OpenConfigFile() => UrdEditorUtils.GetConfigFile(LOGO_CONFIG_FILE_PATH);
    }
}