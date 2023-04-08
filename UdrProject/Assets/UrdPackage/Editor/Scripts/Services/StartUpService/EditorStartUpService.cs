using UnityEditor;
using UnityEngine;

namespace Urd.UrdEditor
{
    public class EditorStartUpService : MonoBehaviour
    {
        private const string STARTUP_CONFIG_FILE_PATH = "Services/StartUpConfig";

        [MenuItem("Urd/StartUpService/StartUpConfiguration", false, 100)]
        public static void OpenConfigFile() => UrdEditorUtils.GetConfigFile(STARTUP_CONFIG_FILE_PATH);
    }
}