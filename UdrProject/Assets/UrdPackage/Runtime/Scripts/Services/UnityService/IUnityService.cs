using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urd.Services
{
    public interface IUnityService : IBaseService
    {
        void OnChangeGamePause(bool pause);
        void OnChangeGameFocus(bool focus);
    }
}