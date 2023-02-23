using UnityEngine;
using Urd.Asset;

namespace Urd.UI
{
    [System.Serializable]
    public class UIProgressBarController : ShowableController<UIProgressBarModel, UIProgressBarView>
    {
        public UIProgressBarController(UIProgressBarModel model) 
            : this(model, null, true) { } 
        public UIProgressBarController(UIProgressBarModel model, Transform parent) 
            : this(model, parent, true) { }
        public UIProgressBarController(UIProgressBarModel model, Transform parent, bool createViewOnAwake) 
            : base (model, parent, createViewOnAwake) { }
        
        public void SetBackgroundSprite(Sprite newSprite)
        {
            Model.SetBackground(newSprite);
        }
        
        public void SetBarSprite(Sprite newSprite)
        {
            Model.SetBar(newSprite);
        }

        public void SetFactor(float factor)
        {
            Model.SetFactor(factor);
        }
    }
}