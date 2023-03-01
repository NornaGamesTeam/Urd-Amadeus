using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Urd.Pool;

namespace Urd.Services.Pool
{
    public class PoolServiceModuleClass
    {
        private Dictionary<Type, IList> _elements = new Dictionary<Type, IList>();

        public PoolServiceModuleClass()
        {
        }

        public void PreLoadClass<T>(int initialAmount) where T : IPoolable
        {
            if (!_elements.TryGetValue(typeof(T), out var list))
            {
                list = new List<T>();
                _elements.Add(typeof(T), list);
            }

            if (list.Count < initialAmount)
            {
                for (int i = initialAmount - list.Count - 1; i >= 0; --i)
                {
                    list.Add(Activator.CreateInstance<T>());
                }
            }
        }

        public T GetObject<T>() where T : IPoolable
        {
            if (!_elements.TryGetValue(typeof(T), out var list))
            {
                PreLoadClass<T>(1);
                return GetObject<T>();
            }

            if (list.Count <= 0)
            {
                list.Add(Activator.CreateInstance<T>());
            }

            T result = (T)list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            result.Init();
            return result;
        }

        public void FreeObject<T>(T objectToFree) where T : IPoolable
        {
            if (_elements.TryGetValue(typeof(T), out var list))
            {
                objectToFree.Dispose();
                list.Add(objectToFree);
            }
            else
            {
                Debug.LogWarning($"[PoolServiceModuleClass] Type {typeof(T)} Not exist in the Pool");
            }
        }
    }
}