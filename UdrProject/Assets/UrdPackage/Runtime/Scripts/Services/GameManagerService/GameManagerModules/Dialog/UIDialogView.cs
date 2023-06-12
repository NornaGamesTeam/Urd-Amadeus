using DG.Tweening;
using TMPro;
using UnityEngine;
using Urd.Dialog;
using Urd.Game;
using Urd.Utils;

namespace Urd.UI.Dialog
{
    public class UIDialogView : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _dialogBoxCanvasGroup;
        [SerializeField]
        private CanvasGroup _arrowCanvasGroup;
        [SerializeField]
        private TextMeshProUGUI _text;

        private DialogModel _dialogModel;

        void Start()
        {
            Init();
        }

        private void Init()
        {
            HideDialogBox(true);
            var dialogModule = StaticServiceLocator.Get<IGameManagerService>().DialogModule;
                dialogModule.OnNewDialog += OnNewDialog;
            dialogModule.OnFinishDialog += OnFinishDialog;
        }

        private void OnFinishDialog()
        {
            _dialogModel.OnChangeVisibleText -= OnChangeVisibleText;
            _dialogModel = null;
            HideDialogBox();
        }

        private void OnNewDialog(DialogModel dialogModel)
        {
            _text.text = string.Empty;

            _dialogModel = dialogModel;
            _dialogModel.OnChangeVisibleText += OnChangeVisibleText;
            ShowDialogBox();
            OnChangeVisibleText();
        }

        private void OnChangeVisibleText()
        {
            _text.DOText(_dialogModel.VisibleText, _dialogModel.CurrentTextDuration).SetEase(_dialogModel.Ease).OnComplete(ShowArrow);
        }

        private void ShowDialogBox()
        {
            _dialogBoxCanvasGroup.DOFade(1, 0.1f);
            _dialogBoxCanvasGroup.interactable = true;
            _arrowCanvasGroup.alpha = 0;
        }
        private void ShowArrow()
        {
            _arrowCanvasGroup.DOFade(1, 0.1f);
        }
        
        private void HideDialogBox(bool forced = false)
        {
            _dialogBoxCanvasGroup.DOFade(0, forced? 0 : 0.1f);
            _arrowCanvasGroup.alpha = 0;
            _dialogBoxCanvasGroup.interactable = false;
            _text.text = string.Empty;
        }
    }
}
