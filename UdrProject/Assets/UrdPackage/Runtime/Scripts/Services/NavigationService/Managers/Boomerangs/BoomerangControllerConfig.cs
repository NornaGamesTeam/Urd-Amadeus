using UnityEngine;
using Urd.View.Boomerang;

namespace Urd.Boomerang
{
    public abstract class BoomerangController<TBoomerangModel> : IBoomerangController 
        where TBoomerangModel : BoomerangModel
    {
        [field: SerializeField]
        public virtual BoomerangBodyView BoomerangBody { get; protected set; }
    }
}