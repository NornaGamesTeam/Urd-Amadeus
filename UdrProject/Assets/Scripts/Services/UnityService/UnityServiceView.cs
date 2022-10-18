using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Urd.Services;
using Urd.Utils;

public class UnityServiceView : MonoBehaviour
{
    IUnityService _unityService;

    void Awake()
    {
        _unityService = StaticServiceLocator.Get<IUnityService>();
    }
}
