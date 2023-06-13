using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Urd.Dialog;
using Urd.Inputs;
using Urd.Services;
using Urd.UI.Dialog;
using Urd.Utils;

namespace Urd.GameManager
{
    public class GameManagerDialogModule
    {
        private const string DIALOG_CONFIG_PATH = "UI/UIDialog/DialogConfigs";
        
        private DialogConfigs _dialogConfigs;
        private DialogController _dialogController;
        
        private ServiceHelper<IInputService> _inputService = new ServiceHelper<IInputService>();

        public event Action<DialogModel> OnNewDialog;
        public event Action OnFinishDialog;

        public GameManagerDialogModule()
        {
            _dialogController = new DialogController();
            _dialogController.OnDialogFinished += OnFinishDialogFinished;

            LoadDialogConfig();
            if (_inputService.HasService)
            {
                SetInitialInput();
            }
            else
            {
                _inputService.OnInitialize += SetInitialInput;
            }
        }
        
        private void SetInitialInput()
        {
            SetUpUIInput(false);
        }
        
        private void LoadDialogConfig()
        {
            _dialogConfigs = Resources.Load<DialogConfigs>(DIALOG_CONFIG_PATH);
        }

        public void ShowDialog(string text) => ShowDialog(new DialogModel(text, _dialogConfigs.GetUIDialog(DialogTypes.Info)));
        public void ShowDialog(DialogModel dialogModel)
        {
            _dialogController.SetDialogModel(dialogModel);

           
            OnNewDialog?.Invoke(dialogModel);
        }

        private void SetUpUIInput(bool enableUI)
        {
            
            var inputService = StaticServiceLocator.Get<IInputService>();

            // move this to a proper site
            inputService.ChangeAvailabilityOfActionMap(InputActionMapTypes.Character, !enableUI);
            inputService.ChangeAvailabilityOfActionMap(InputActionMapTypes.UI, enableUI);
        }

        private void OnFinishDialogFinished()
        {
            OnFinishDialog?.Invoke();
            SetUpUIInput(false);
        }
    }
}