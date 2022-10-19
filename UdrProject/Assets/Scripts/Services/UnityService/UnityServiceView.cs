using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Urd.Services;
using Urd.Utils;

public class UnityServiceView : MonoBehaviour
{
    private IUnityService _unityService;

    void Awake()
    {
        _unityService = StaticServiceLocator.Get<IUnityService>();
    }

    private void OnApplicationFocus(bool focus)
    {
        _unityService.OnChangeGameFocus(focus);
    }

    private void OnApplicationPause(bool pause)
    {
        _unityService.OnChangeGamePause(pause);
    }
}
