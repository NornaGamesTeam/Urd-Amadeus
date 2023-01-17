using UnityEngine;
using Urd.Scene;
using Urd.Services;
using Urd.Utils;

public class DebugOpenScene : MonoBehaviour
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
            StaticServiceLocator.Get<INavigationService>().Open(sceneModel, null);
        }
    }
}
