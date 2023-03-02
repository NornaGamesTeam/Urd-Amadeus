using UnityEditor;
using UnityEngine;

namespace Urd.UrdEditor
{
    public class EditorNavigationBoomerangManager : MonoBehaviour
    {
        private const string BOOMERANG_CONFIG_FILE_PATH = "BoomerangTypesConfig";

        [MenuItem("Urd/Navigation/BoomerangsConfigFile", false, 100)]
        public static void OpenConfigFile() => UrdEditorUtils.GetConfigFile(BOOMERANG_CONFIG_FILE_PATH);
    }
}