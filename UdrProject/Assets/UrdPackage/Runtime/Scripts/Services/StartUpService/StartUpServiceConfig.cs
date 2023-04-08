using System.Collections.Generic;
using UnityEngine;

namespace Urd.Services
{
    [CreateAssetMenu(fileName = "StartUpConfig", menuName = "Urd/StartUp Config", order = 1)]
    public class StartUpServiceConfig : ScriptableObject
    {
        [field: SerializeReference, SubclassSelector]
        public List<IBaseService> BaseServices { get; private set; }
    }
}