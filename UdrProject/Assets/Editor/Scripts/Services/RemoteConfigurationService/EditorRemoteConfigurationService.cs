using UnityEditor;
using UnityEngine;

namespace Urd.UrdEditor
{
    public class EditorRemoteConfigurationService : MonoBehaviour
    {
        private const string REMOTE_CONFIG_FILE_PATH = "RemoteConfiguration";

        [MenuItem("Urd/RemoteConfiguration/RemoteConfiguration")]
        public static void OpenConfigFile() => UrdEditorUtils.GetConfigFile(REMOTE_CONFIG_FILE_PATH);
    }
}