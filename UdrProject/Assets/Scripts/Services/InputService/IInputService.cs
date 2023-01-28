using System;
using UnityEngine.InputSystem;

namespace Urd.Services
{
    public interface IInputService : IBaseService
    {
        void SubscribeToAction(string actionName, Action<InputAction.CallbackContext> onPerformMethod);
        void UnsubscribeToAction(string actionName, Action<InputAction.CallbackContext> onPerformMethod);
    }
}