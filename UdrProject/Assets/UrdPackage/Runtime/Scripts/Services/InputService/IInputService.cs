using System;
using UnityEngine.InputSystem;

namespace Urd.Services
{
    public interface IInputService : IBaseService
    {
        void SubscribeToActionOnPerformed(string actionName, Action<InputAction.CallbackContext> onPerformMethod);
        void UnsubscribeToActionOnPerformed(string actionName, Action<InputAction.CallbackContext> onPerformMethod);
        
        void SubscribeToActionOnCancel(string actionName, Action<InputAction.CallbackContext> onPerformMethod);
        void UnsubscribeToActionOnCancel(string actionName, Action<InputAction.CallbackContext> onPerformMethod);
    }
}