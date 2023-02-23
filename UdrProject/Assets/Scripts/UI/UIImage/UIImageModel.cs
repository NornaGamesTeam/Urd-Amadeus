using UnityEngine;
using Urd.Asset;

namespace Urd.UI
{
    [System.Serializable]
    public class UIImageModel : ShowableModel
    {
        protected override string DEFAULT_ADDRESSABLE => "DefaultUIImage";
        
        [field: SerializeField, PreviewSprite]
        public Sprite Sprite { get; private set; }

        [field: SerializeField]
        public Color Color { get; private set; }

        public UIImageModel(Sprite sprite) : this(sprite, string.Empty) { }

        private UIImageModel(Sprite sprite, string addressable) : base(addressable) 
        {
            Sprite = sprite;
        }

        public void SetSprite(Sprite newSprite)
        {
            Sprite = newSprite;
            CallOnChanged();
        }
        
        public void SetColor(Color newColor)
        {
            Color = newColor;
            CallOnChanged();
        }
    }
}