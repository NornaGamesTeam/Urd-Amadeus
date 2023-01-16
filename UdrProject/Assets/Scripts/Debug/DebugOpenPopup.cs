using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Urd.Popup;
using Urd.Services;
using Urd.Utils;

public class DebugOpenPopup : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            dynamic popupInfoModel = new PopupInfoModel();
            StaticServiceLocator.Get<INavigationService>().Open(popupInfoModel, null);
        }
    }

    private void OnOpenPopup(bool success)
    {
        Debug.Log($" {success}");
    }
}
