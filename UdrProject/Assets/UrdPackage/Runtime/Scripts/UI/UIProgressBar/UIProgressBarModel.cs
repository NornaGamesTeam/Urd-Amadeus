using System;
using Unity.Collections;
using UnityEngine;
using Urd.Asset;

namespace Urd.UI
{
    [System.Serializable]
    public class UIProgressBarModel : ShowableModel
    {
        protected override string DEFAULT_ADDRESSABLE => "DefaultUIProgressBar";
        
        [field: SerializeField] 
        public UIImageModel Background { get; private set; }
        [field: SerializeField] 
        public UIImageModel Bar { get; private set; }
        
        [field: SerializeField] 
        public float Factor { get; private set; }

        public event Action OnFactorChanged;

        public UIProgressBarModel(UIImageModel backgroundModel, UIImageModel barModel) 
            : this(backgroundModel, barModel, string.Empty) { }
        
        private UIProgressBarModel(UIImageModel backgroundModel, UIImageModel barModel, string addressable) 
            : base(addressable)
        {
            Background = backgroundModel;
            Bar = barModel;
        }

        public void SetBackground(Sprite newBackgroundSprite)
        {
            SetProperties(newBackgroundSprite, Bar.Sprite);
        }

        public void SetBar(Sprite newBarSprite)
        {
            SetProperties(Background.Sprite, newBarSprite);
        }
        private void SetProperties(Sprite newBackgroundSprite, Sprite newBarSprite)
        {
            Background.SetSprite(newBackgroundSprite);
            Bar.SetSprite(newBarSprite);
            CallOnChanged();
        }

        public void SetFactor(float factor)
        {
            Factor = factor;
            Bar.SetImageFillFactor(Factor);
            OnFactorChanged?.Invoke();
        }
    }
}
