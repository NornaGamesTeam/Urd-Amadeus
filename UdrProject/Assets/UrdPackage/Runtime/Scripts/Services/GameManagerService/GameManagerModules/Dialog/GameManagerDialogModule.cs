using System;
using UnityEngine;
using Urd.Dialog;
using Urd.UI.Dialog;

namespace Urd.GameManager
{
    public class GameManagerDialogModule
    {
        private const string DIALOG_CONFIG_PATH = "UI/UIDialog/DialogConfigs";

        private DialogConfigs _dialogConfigs;
        private DialogController _dialogController;

        public GameManagerDialogModule()
        {
            _dialogController = new DialogController();
            _dialogController.OnDialogFinished += OnFinishDialogFinished;
            LoadDialogConfig();
        }

        public event Action<DialogModel> OnNewDialog;
        public event Action OnFinishDialog;

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
        
        private void OnFinishDialogFinished()
        {
            OnFinishDialog?.Invoke();
        }
    }
}