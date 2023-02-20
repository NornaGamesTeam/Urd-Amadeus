using UnityEngine;
using UnityEngine.Serialization;
using Urd.Services.Asset;

namespace Urd.UI
{
    [System.Serializable]
    public class UIImageModel : IShoweable
    {
        private const string DEFAULT_ADDRESSABLE = "DefaultUIImage";
        [field: SerializeField, PreviewSprite] public Sprite Sprite { get; private set; }

        [SerializeField] public string _addressable;
        public string Addressable => !string.IsNullOrEmpty(_addressable) ? _addressable : DEFAULT_ADDRESSABLE;

        public event System.Action OnSpriteSet;

        public UIImageModel(Sprite sprite) : this(sprite, DEFAULT_ADDRESSABLE)
        {
        }

        private UIImageModel(Sprite sprite, string addressable)
        {
            Sprite = sprite;
            _addressable = addressable;
        }

        public void SetSprite(Sprite newSprite)
        {
            Sprite = newSprite;
            OnSpriteSet?.Invoke();
        }
    }
}