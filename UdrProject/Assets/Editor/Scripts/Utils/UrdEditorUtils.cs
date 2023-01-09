using UnityEditor;
using UnityEngine;

namespace Urd.UrdEditor
{
    public class UrdEditorUtils
    {
        public static void GetConfigFile(string configFilePath)
        {
            var allPrefabs = AssetDatabase.FindAssets(configFilePath);
            for (int i = 0; i < allPrefabs.Length; i++)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(allPrefabs[i]);
                var prefabObject = AssetDatabase.LoadMainAssetAtPath(assetPath);

                var prefabGameObject = prefabObject as ScriptableObject;
                if (prefabGameObject != null)
                {
                    Selection.activeObject = prefabGameObject;
                    return;
                }
            }
            Debug.LogWarning($"Config File not found : '{configFilePath}'");
        }
    }
}