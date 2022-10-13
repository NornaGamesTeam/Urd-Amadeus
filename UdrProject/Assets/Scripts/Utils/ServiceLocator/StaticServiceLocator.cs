using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Urd.Services;

namespace Urd.Utils
{
    public static class StaticServiceLocator
    {
        private static Dictionary<Type, IBaseService>
            Services = new Dictionary<Type, IBaseService>();

        public static T Register<T>(T serviceInstance) where T : IBaseService
        {
            Services[typeof(T)] = serviceInstance;

            return serviceInstance;
        }

        public static bool Exist<T>()
        {
            return Services.ContainsKey(typeof(T));
        }

        public static T Get<T>() where T : IBaseService
        {
            if (!Services.TryGetValue(typeof(T), out var newInstance))
            {
                Debug.LogWarning($"Service of type {typeof(T)} not registered, trying to create a default one");
            }

            return (T)newInstance;
        }

        public static void Reset()
        {
            Services.Clear();
        }
    }
}