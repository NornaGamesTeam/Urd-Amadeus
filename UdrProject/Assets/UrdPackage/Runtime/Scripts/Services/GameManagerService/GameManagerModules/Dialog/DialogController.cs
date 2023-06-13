using System;
using UnityEngine.InputSystem;
using Urd.Dialog;
using Urd.Services;
using Urd.Timer;
using Urd.Utils;

namespace Urd.UI.Dialog
{
    public class DialogController
    {
        private const string INPUT_DIALOG_BOX_ACTION = "DialogBox";

        private DialogModel _dialogModel;

        private float _timeStamp;

        private ServiceHelper<IClockService> _clockService = new ServiceHelper<IClockService>();
        private ServiceHelper<IInputService> _inputService = new ServiceHelper<IInputService>();

        private TimerModel _timerModel;
        
        public event Action OnDialogFinished;
        
        public DialogController()
        {
            SubscribeToUIInput();
        }

        private void SubscribeToUIInput()
        {
            if (_inputService.HasService)
            {
                _inputService.Service.SubscribeToActionOnPerformed(INPUT_DIALOG_BOX_ACTION, OnUseDialogBox);
            }
            else
            {
                _inputService.OnInitialize += SubscribeToUIInput;
            }
        }
        
        public void SetDialogModel(DialogModel dialogModel)
        {
            _dialogModel = dialogModel;
            BeginDialog();
        }

        private void BeginDialog()
        {
            _dialogModel.BeginDialog();
            SetSubString();
        }

        private void SetSubString()
        {
            _dialogModel.SetSubString();
            _timerModel = _clockService.Service.AddDelayCall(_dialogModel.CurrentTextDuration, FinishCurrentText);
        }

        private void FinishCurrentText()
        {
            _timerModel = null;
            
            _dialogModel.FinishCurrentText();
        }

        private void OnUseDialogBox(InputAction.CallbackContext context)
        {
            if (_timerModel?.IsInCooldown == true)
            {
                _timerModel.ForceFinish();
                return;
            }

            if (_dialogModel.IsFinished)
            {
                FinishDialog();
                return;
            }
            
            ContinueText();
        }

        private void ContinueText()
        {
            _dialogModel.ContinueText();
            SetSubString();
        }

        private void FinishDialog()
        {
            _dialogModel.FinishDialog();
            OnDialogFinished?.Invoke();
        }
    }
}
