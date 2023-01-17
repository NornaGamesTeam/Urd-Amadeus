using UnityEngine;
using Urd.Boomerang;
using Urd.Services;
using Urd.Utils;

public class DebugOpenBoomerang : MonoBehaviour
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
            var popupInfoModel = new BoomerangInfoModel();
            StaticServiceLocator.Get<INavigationService>().Open(popupInfoModel, null);
        }
    }

    private void OnOpenBoomerang(bool success)
    {
        Debug.Log($"Boomerang Opened {success}");
    }
}
