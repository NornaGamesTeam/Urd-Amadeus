using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Urd.Services
{
    public class InputService : BaseService, IInputService
    {
        public override void Init()
        {
            base.Init();

            LoadAllInputs();
        }

        private void LoadAllInputs()
        {
            /*
            var list = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                                .Where(x => typeof(IInputActionCollection2).IsAssignableFrom(x) && !x.IsInterface &&
                                            !x.IsAbstract)
                                .Select(x => x.Name).ToList();
            Debug.Log("list");
            */
        }

        private List<Type> GetTypesThatImplement<T>()
        {
            var list = new List<Type>();
            var typeT = typeof(T);
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (typeT.IsAssignableFrom(type) && !type.IsInterface)
                {
                    list.Add(type);
                }
            }

            return list;
        }
    }
}
