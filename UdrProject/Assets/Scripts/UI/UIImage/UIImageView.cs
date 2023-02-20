using System;
using UnityEngine;
using UnityEngine.UI;

namespace Urd.UI
{
    [RequireComponent(typeof(Image))]
    public class UIImageView : MonoBehaviour
    {
        [SerializeField]
        private UIImageModel _model;
        
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetModel(UIImageModel model)
        {
            _model = model;
            OnSpriteSet();
            
            _model.OnSpriteSet += OnSpriteSet;
        }

        private void OnDestroy()
        {
            if (_model != null)
            {
                _model.OnSpriteSet -= OnSpriteSet;
            }
        }

        private void OnSpriteSet()
        {
            _image.sprite = _model.Sprite;
        }
    }
}
