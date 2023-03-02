using UnityEngine;
using Urd.Asset;

namespace Urd.UI
{
    [System.Serializable]
    public class UIImageController : ShowableController<UIImageModel, UIImageView>
    {
        public UIImageController(UIImageModel model) : this(model, null, true) { } 
        public UIImageController(UIImageModel model, Transform parent) : this(model, parent, true) { }
        public UIImageController(UIImageModel model, Transform parent, bool createViewOnAwake) :base (model, parent, createViewOnAwake) { }
        
        public void SetSprite(Sprite newSprite)
        {
            Model.SetSprite(newSprite);
        }
    }
}