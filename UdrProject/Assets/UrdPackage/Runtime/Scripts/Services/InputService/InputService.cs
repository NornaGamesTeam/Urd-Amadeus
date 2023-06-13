using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Urd.Inputs;
using Urd.Utils;

namespace Urd.Services
{
    [Serializable]
    public class InputService : BaseService, IInputService
    {
        private List<InputAction> _actions = new List<InputAction>();
        private List<InputActionHold> _holdActions = new ();

        private IClockService _clockService;
        public override void Init()
        {
            base.Init();

            _clockService = StaticServiceLocator.Get<IClockService>();
            _clockService.SubscribeToUpdate(CustomUpdate);
            
            LoadAllCustomInteractions();
            LoadAllInputs();
            SetAsLoaded();
        }

        private void LoadAllCustomInteractions()
        {
            var types = AssemblyHelper.GetClassTypesThatImplement<IInputInteraction>();
            for (int i = types.Count - 1; i >= 0; i--)
            {
                InputSystem.RegisterInteraction(types[i]);
            }
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

        public void SubscribeToActionOnHold(string actionName, Action<InputAction.CallbackContext> onHoldMethod)
        {
            var allActions = _actions.FindAll(action => action.name == actionName);
            for (int i = allActions.Count - 1; i >= 0; i--)
            {
                allActions[i].performed += OnHoldBegin;
                allActions[i].canceled += OnHoldEnd;
                _holdActions.Add(new InputActionHold(allActions[i], onHoldMethod));
            }
        }


        public void UnsubscribeToActionOnHold(string actionName, Action<InputAction.CallbackContext> onHoldMethod)
        {
            var allActions = _actions.FindAll(action => action.name == actionName);
            for (int i = allActions.Count - 1; i >= 0; i--)
            {
                allActions[i].performed -= OnHoldBegin;
                allActions[i].canceled -= OnHoldEnd;
                _holdActions.RemoveAll(action => action.InputAction == allActions[i]);
            }
        }

        public void ChangeAvailabilityOfActionMap(InputActionMapTypes actionMapType, bool enabled)
        {
            for (int i = 0; i < _actions.Count; i++)
            {
                if (_actions[i].actionMap.name == actionMapType.ToString())
                {
                    if (enabled)
                    {
                        _actions[i].actionMap.Enable();
                    }
                    else
                    {
                        _actions[i].actionMap.Disable();
                    }
                }
            }
        }


        private void OnHoldBegin(InputAction.CallbackContext context)
        {
            var holdAction = _holdActions.Find(action => action.InputAction == context.action);
            holdAction.SetIsHolding(true, context);
        }

        void CustomUpdate(float deltaTime)
        {
            for (int i = 0; i < _holdActions.Count; i++)
            {
                if (_holdActions[i].IsHolding)
                {
                    _holdActions[i].CallBack?.Invoke(_holdActions[i].Context);
                }
            }
        }
        
        private void OnHoldEnd(InputAction.CallbackContext context)
        {
            var holdAction = _holdActions.Find(action => action.InputAction == context.action);
            holdAction.SetIsHolding(false, context);
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

    internal class InputActionHold
    {
        public InputAction InputAction { get; private set; }
        public Action<InputAction.CallbackContext> CallBack { get; private set; }
        public bool IsHolding { get; private set; }
        public InputAction.CallbackContext Context { get; private set; }

        public InputActionHold(InputAction inputAction, Action<InputAction.CallbackContext> callBack)
        {
            InputAction = inputAction;
            CallBack = callBack;
        }

        public void SetIsHolding(bool isHolding, InputAction.CallbackContext context = default)
        {
            IsHolding = isHolding;
            Context = context;
        }
    }
}
