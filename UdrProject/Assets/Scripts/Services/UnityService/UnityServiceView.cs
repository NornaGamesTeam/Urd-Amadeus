using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Urd.Services;
using Urd.Utils;

public class UnityServiceView : MonoBehaviour
{
    private ServiceHelper<IUnityService> _unityService = new ServiceHelper<IUnityService>();

    private void OnApplicationFocus(bool focus)
    {
        _unityService.Service?.OnChangeGameFocus(focus);
    }

    private void OnApplicationPause(bool pause)
    {
        _unityService.Service?.OnChangeGamePause(pause);
    }
}
