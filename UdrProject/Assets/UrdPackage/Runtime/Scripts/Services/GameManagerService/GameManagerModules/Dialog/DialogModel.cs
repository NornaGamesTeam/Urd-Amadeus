using System;
using DG.Tweening;
using Urd.UI.Dialog;

namespace Urd.Dialog
{
    public class DialogModel : IDisposable
    {
        private const string SPLIT_CHARACTER = "#"; 
        
        public string Text { get; private set; }

        private UIDialogConfig _dialogConfig;

        public string VisibleText { get; private set; }
        public float CurrentTextDuration => VisibleText.Length * +_dialogConfig.TimePerLetter;
        public bool IsFinished => _currentSubString >= Text.Split(SPLIT_CHARACTER).Length-1;
        public Ease Ease => _dialogConfig.Ease;

        public event Action OnChangeVisibleText;
        public event Action OnSubStringFinished;
        public delegate void DelegateOnChangeVisibleTextAmount(bool isShowingCompleted);
        public event DelegateOnChangeVisibleTextAmount OnChangeVisibleTextAmount;

        private int _currentSubString;
        
        public DialogModel(string text, UIDialogConfig dialogConfig)
        {
            Text = text;
            _dialogConfig = dialogConfig;
        }

        public void Dispose()
        {
        }

        public void BeginDialog()
        {
            _currentSubString = 0;
        }

        public void SetSubString()
        {
            VisibleText = Text.Split(SPLIT_CHARACTER)[_currentSubString];
            OnChangeVisibleText?.Invoke();
        }
        
        public void ContinueText()
        {
            _currentSubString++;
        }

        public void FinishCurrentText()
        {
            OnSubStringFinished?.Invoke();
        }

        public void FinishDialog()
        {
            
        }
    }
}