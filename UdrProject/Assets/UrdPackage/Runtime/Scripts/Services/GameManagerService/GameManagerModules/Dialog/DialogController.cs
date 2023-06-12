using System;
using MyBox;
using UnityEngine;
using Urd.Dialog;
using Urd.Services;
using Urd.Utils;

namespace Urd.UI.Dialog
{
    public class DialogController
    {
        private DialogModel _dialogModel;

        private IClockService _clockService;

        private float _timeStamp;
        
        public DialogController()
        {
            _clockService = StaticServiceLocator.Get<IClockService>();
        }

        public event Action OnDialogFinished;

        public void SetDialogModel(DialogModel dialogModel)
        {
            _dialogModel = dialogModel;
            BeginDialog();
        }

        private void BeginDialog()
        {
            _dialogModel.BeginDialog();

            BeginSubString();
        }

        private void BeginSubString()
        {
            _dialogModel.SetSubString();
            
            _clockService.AddDelayCall(_dialogModel.CurrentTextDuration, FinishCurrentText);
        }

        private void FinishCurrentText()
        {
            _dialogModel.FinishCurrentText();

            if (_dialogModel.IsFinished)
            {
                WaitForInput(FinishDialog);
                return;
            }
            WaitForInput(ContinueText);
        }

        public void WaitForInput(Action onCallback)
        {
            //onCallback?.Invoke();
        }

        private void ContinueText()
        {
            _dialogModel.ContinueText();
        }

        private void FinishDialog()
        {
            _dialogModel.FinishDialog();
            OnDialogFinished?.Invoke();
        }
    }
}
