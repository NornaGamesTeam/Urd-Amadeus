using System;
using UnityEngine;
using UnityEngine.UI;
using Urd.Asset;

namespace Urd.UI
{
    [RequireComponent(typeof(Image))]
    public class UIImageView : ShowableView<UIImageModel>
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        protected override void OnModelSet()
        {
            SetImage();
        }

        protected override void OnModelChanged()
        {
            SetImage();
        }

        private void SetImage()
        {
            _image.sprite = Model.Sprite;
        }
    }
}
