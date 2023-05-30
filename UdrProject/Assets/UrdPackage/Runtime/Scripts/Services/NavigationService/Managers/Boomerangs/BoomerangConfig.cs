using UnityEngine;
using Urd.View.Boomerang;

namespace Urd.Boomerang
{
    [CreateAssetMenu(fileName = "BoomerangConfig", menuName = "Urd/Navigation/NewBoomerangConfig", order = 1)]
    public class BoomerangConfig : ScriptableObject
    {
        [field: SerializeReference, SubclassSelector]
        public BoomerangModel BoomerangModel { get; private set; }
        [field: SerializeField]
        public BoomerangViewNoModel BoomerangView { get; private set; }
        [field: SerializeReference, SubclassSelector]
        public IBoomerangController BoomerangController { get; private set; }
        
    }
}
