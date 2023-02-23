using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using Urd.Utils;

namespace Urd.Services
{
    public class InputService : BaseService, IInputService
    {
        private List<InputAction> _actions = new List<InputAction>();
        
        public override void Init()
        {
            base.Init();

            LoadAllInputs();
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

        public void SubscribeToAction(string actionName, Action<InputAction.CallbackContext> onPerformMethod)
        {
            var allActions = _actions.FindAll(action => action.name == actionName);
            for (int i = allActions.Count - 1; i >= 0; i--)
            {
                allActions[i].performed += onPerformMethod;
            }
        }

        public void UnsubscribeToAction(string actionName, Action<InputAction.CallbackContext> onPerformMethod)
        {
            var allActions = _actions.FindAll(action => action.name == actionName);
            for (int i = allActions.Count - 1; i >= 0; i--)
            {
                allActions[i].performed -= onPerformMethod;
            }
        }
    }
}
