using UnityEngine;

namespace Urd.Asset
{
    public class ShowableView<TModel> : MonoBehaviour, IShowableView<TModel>
        where TModel : IShowableModel
    {
        public TModel Model { get; private set; }

        public void SetModel(TModel model)
        {
            Model = model;

            Model.OnChanged += OnModelChanged;
            OnModelSet();
        }
        
        private void OnDestroy()
        {
            if (Model != null)
            {
                Model.OnChanged -= OnModelChanged;
            }
        }

        protected virtual void OnModelChanged() { }
        protected virtual void OnModelSet() { }
    }
}
