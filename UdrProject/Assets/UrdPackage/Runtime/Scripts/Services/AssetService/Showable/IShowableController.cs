using UnityEngine;
using Urd.Asset;

namespace Urd.Asset
{
    public interface IShowableController<TModel, TView> 
        where TModel: IShowableModel
        where TView: IShowableView<TModel>
    {
        public TModel Model { get; }
        public TView View { get; }
        public Transform Parent { get; }
    }
}