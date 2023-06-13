using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Urd.Dialog;
using Urd.Inputs;
using Urd.Scene;
using Urd.Services;
using Urd.Services.Navigation;
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
        private ServiceHelper<INavigationService> _navigationService = new ServiceHelper<INavigationService>();

        public event Action<DialogModel> OnNewDialog;
        public event Action OnFinishDialog;

        public GameManagerDialogModule()
        {
            _dialogController = new DialogController();
            _dialogController.OnDialogFinished += OnFinishDialogFinished;

            LoadDialogConfig();
            if (_navigationService.HasService)
            {
                OnSceneChanged();
            }
            else
            {
                _navigationService.OnInitialize += OnSceneChanged;
            }
        }
        
        private void OnSceneChanged()
        {
            _navigationService.Service.OnFinishLoadNavigable += OnSceneChanged;
        }

        private void OnSceneChanged(INavigable iNavigable)
        {
            var sceneModel = iNavigable as SceneModel;
            if (sceneModel != null && sceneModel.Id == SceneTypes.Game.ToString())
            {
                SetUpUIInput(false);
            }
        }

        private void LoadDialogConfig()
        {
            _dialogConfigs = Resources.Load<DialogConfigs>(DIALOG_CONFIG_PATH);
        }

        public void ShowDialog(string text) => ShowDialog(new DialogModel(text, _dialogConfigs.GetUIDialog(DialogTypes.Info)));
        public void ShowDialog(DialogModel dialogModel)
        {
            _dialogController.SetDialogModel(dialogModel);
            SetUpUIInput(true);
            OnNewDialog?.Invoke(dialogModel);
        }

        private void SetUpUIInput(bool enableUI)
        {
            _inputService.Service.ChangeAvailabilityOfActionMap(InputActionMapTypes.Character, !enableUI);
            _inputService.Service.ChangeAvailabilityOfActionMap(InputActionMapTypes.UI, enableUI);
        }

        private void OnFinishDialogFinished()
        {
            OnFinishDialog?.Invoke();
            SetUpUIInput(false);
        }
    }
}