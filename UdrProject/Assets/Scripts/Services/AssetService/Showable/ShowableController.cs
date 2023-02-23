using UnityEngine;
using Urd.Services;
using Urd.Utils;

namespace Urd.Asset
{
    public abstract class ShowableController<TModel, TView> : IShowableController<TModel, TView> 
        where TModel : IShowableModel
        where TView : IShowableView<TModel>
    {
        [field:SerializeField, ReadOnly]
        public TModel Model { get; private set; }
        public TView View { get; private set; }
        public Transform Parent { get; private set; }
        
        private ServiceHelper<IAssetService> _iAssetService = new ();

        protected ShowableController(TModel model, Transform parent, bool createViewOnAwake)
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
            _iAssetService.Service.Instantiate(Model.Addressable, Parent, OnViewLoaded);
        }
        
        private void OnViewLoaded(GameObject view)
        {
            if (view == null)
            {
                return;
            }
            
            View = view.GetComponent<TView>();
            View?.SetModel(Model);
        }
    }
}