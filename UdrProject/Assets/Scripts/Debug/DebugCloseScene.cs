using UnityEngine;
using Urd.Scene;
using Urd.Services;
using Urd.Utils;

public class DebugCloseScene : MonoBehaviour
{
    [SerializeField] 
    private bool _enabled;

    [SerializeField] 
    private KeyCode _keyCode;
    
    void Update()
    {
        if (!_enabled)
        {
            return;
        }
        
        if (Input.GetKeyDown(_keyCode))
        {
            var sceneModel = new SceneModel(SceneTypes.Info);
            StaticServiceLocator.Get<INavigationService>().Close(sceneModel, null);
        }
    }
}
