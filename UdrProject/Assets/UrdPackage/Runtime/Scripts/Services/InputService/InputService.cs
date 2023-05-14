using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Urd.Services
{
    [Serializable]
    public class InputService : BaseService, IInputService
    {
        private List<InputAction> _actions = new List<InputAction>();
        
        public override void Init()
        {
            base.Init();

            LoadAllInputs();
            SetAsLoaded();
        }

        private void LoadAllInputs()
        {
            var types = AssemblyHelper.GetClassTypesThatImplement<IInputActionCollection2>();
            for (int i = types.Count - 1; i >= 0; i--)
            {
                var newInputDefinition = Activator.CreateInstance(types[i]) as IInputActionCollection2;

                SaveActions(newInputDefinition);
            }
        }

        private void SaveActions(IInputActionCollection2 newInputDefinition)
        {
            foreach (var inputAction in newInputDefinition)
            {
                inputAction.Enable();
                _actions.Add(inputAction);
            }
        }

        public void SubscribeToActionOnStarted(string actionName, Action<InputAction.CallbackContext> onStartMethod)
        {
            var allActions = _actions.FindAll(action => action.name == actionName);
            for (int i = allActions.Count - 1; i >= 0; i--)
            {
                allActions[i].started += onStartMethod;
            }
        }
        
        public void UnsubscribeToActionOnStarted(string actionName, Action<InputAction.CallbackContext> onStartMethod)
        {
            var allActions = _actions.FindAll(action => action.name == actionName);
            for (int i = allActions.Count - 1; i >= 0; i--)
            {
                allActions[i].started -= onStartMethod;
            }
        }
        
        public void SubscribeToActionOnPerformed(string actionName, Action<InputAction.CallbackContext> onPerformMethod)
        {
            var allActions = _actions.FindAll(action => action.name == actionName);
            for (int i = allActions.Count - 1; i >= 0; i--)
            {
                allActions[i].performed += onPerformMethod;
            }
        }
        
        public void UnsubscribeToActionOnPerformed(string actionName, Action<InputAction.CallbackContext> onPerformMethod)
        {
            var allActions = _actions.FindAll(action => action.name == actionName);
            for (int i = allActions.Count - 1; i >= 0; i--)
            {
                allActions[i].performed -= onPerformMethod;
            }
        }
        public void SubscribeToActionOnCancel(string actionName, Action<InputAction.CallbackContext> onPerformMethod)
        {
            var allActions = _actions.FindAll(action => action.name == actionName);
            for (int i = allActions.Count - 1; i >= 0; i--)
            {
                allActions[i].canceled += onPerformMethod;
            }
        }
        
        public void UnsubscribeToActionOnCancel(string actionName, Action<InputAction.CallbackContext> onPerformMethod)
        {
            var allActions = _actions.FindAll(action => action.name == actionName);
            for (int i = allActions.Count - 1; i >= 0; i--)
            {
                allActions[i].canceled -= onPerformMethod;
            }
        }
    }
}
