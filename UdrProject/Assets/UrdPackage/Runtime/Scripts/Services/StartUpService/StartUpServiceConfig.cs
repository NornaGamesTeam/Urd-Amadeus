using System;
using System.Collections.Generic;
using UnityEngine;

namespace Urd.Services
{
    [CreateAssetMenu(fileName = "StartUpConfig", menuName = "Urd/StartUp Config", order = 1)]
    public class StartUpServiceConfig : ScriptableObject
    {
        [field: SerializeReference, SubclassSelector]
        public List<IBaseService> BaseServices { get; private set; }

        [ContextMenu("FillWithAllServices")]
        public void FillWithAllServices()
        {
                BaseServices.Clear();
            var types = AssemblyHelper.GetClassTypesThatImplement<IBaseService>();
            for (int i = 0; i < types.Count; i++)
            {
                var baseService = Activator.CreateInstance(types[i]) as IBaseService;
                BaseServices.Add(baseService);
            }
        }
    }
}