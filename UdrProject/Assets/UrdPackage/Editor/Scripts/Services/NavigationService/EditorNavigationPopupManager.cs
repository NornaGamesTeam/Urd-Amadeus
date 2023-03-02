using UnityEditor;
using UnityEngine;

namespace Urd.UrdEditor
{
    public class EditorNavigationPopupManager : MonoBehaviour
    {
        private const string POPUP_CONFIG_FILE_PATH = "PopupTypesConfig";

        [MenuItem("Urd/Navigation/PopupsConfigFile", false, 100)]
        public static void OpenConfigFile() => UrdEditorUtils.GetConfigFile(POPUP_CONFIG_FILE_PATH);
    }
}