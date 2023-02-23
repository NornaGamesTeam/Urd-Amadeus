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

        public static void Register<T>(T serviceInstance) where T : IBaseService
        {
            Register(serviceInstance, typeof(T));
        }
        
        public static void Register(IBaseService serviceInstance, Type type)
        {
            Services[type] = serviceInstance;
        }

        public static bool Exist<T>() => Exist(typeof(T));
        public static bool Exist(Type type)
        {
            return Services.ContainsKey(type);
        }

        public static T Get<T>() where T : IBaseService
        {
            if (!Services.TryGetValue(typeof(T), out var newInstance))
            {
                Debug.LogWarning($"Service of type {typeof(T)} not registered");
            }

            return (T)newInstance;
        }

        public static void Reset()
        {
            Services.Clear();
        }
    }
}