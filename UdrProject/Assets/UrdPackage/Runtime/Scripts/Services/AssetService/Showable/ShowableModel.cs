using UnityEngine;

namespace Urd.Asset
{
    [System.Serializable]
    public abstract class ShowableModel : IShowableModel
    {
        protected virtual string DEFAULT_ADDRESSABLE => string.Empty;
        
        
        [SerializeField, HideInInspector] 
        protected bool _customAddressable;
        [SerializeField] 
        protected string _addressable;
        public virtual string Addressable => !string.IsNullOrEmpty(_addressable) ? _addressable : DEFAULT_ADDRESSABLE;

        public event System.Action OnChanged;

        public ShowableModel(string addressable)
        {
            _addressable = addressable;
        }
        protected void CallOnChanged()
        {
            OnChanged?.Invoke();
        }
    }
}