using System;
using UnityEngine;
using UnityEngine.UI;
using Urd.Asset;

namespace Urd.UI
{
    public class UIProgressBarView : ShowableView<UIProgressBarModel>
    {
        [SerializeField]
        private Image _background;

        [SerializeField]
        private Image _bar;
        
        protected override void OnModelSet()
        {
            OnModelChanged();
            Model.OnFactorChanged += SetFactor;
        }
        
        protected override void OnModelChanged()
        {
            SetFactor();
            SetImages();
        }

        private void SetImages()
        {
            _background.sprite = Model.Background.Sprite;
            _bar.sprite = Model.Bar.Sprite;
        }
        
        private void SetFactor()
        {
            _bar.fillAmount = Model.Bar.FillFactor;
        }
    }
}
