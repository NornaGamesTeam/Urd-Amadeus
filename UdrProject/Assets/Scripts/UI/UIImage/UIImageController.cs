
using UnityEngine;
using Urd.Services;
using Urd.Utils;

namespace Urd.UI
{
    public class UIImageController
    {
        public UIImageModel Model { get; private set; }
        public UIImageView View { get; private set; }
        public Transform Parent { get; private set; }
        
        private ServiceHelper<IAssetService> _iAssetService = new ();
        
        public UIImageController(UIImageModel model) : this(model, null, true) { } 
        public UIImageController(UIImageModel model, Transform parent) : this(model, parent, true) { }
        public UIImageController(UIImageModel model, Transform parent, bool createViewOnAwake)
        {
            Model = model;
            Parent = parent;

            if (createViewOnAwake)
            {
                CreateView();
            }
        }

        public void CreateView()
        {
            _iAssetService.Service.Instantiate(Model.Addressable, Parent, OnBackgroundLoaded);
        }
        
        private void OnBackgroundLoaded(GameObject newBackground)
        {
            View =newBackground.GetComponent<UIImageView>();
            View.SetModel(Model);
        }

        public void SetSprite(Sprite newSprite)
        {
            Model.SetSprite(newSprite);
        }
    }
}