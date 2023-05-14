using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Services;

namespace Urd.Utils
{
    public static class StaticServiceLocator
    {
        private static Dictionary<Type, IBaseService> Services;

        private static Dictionary<Type, DelegateHelper.DelegateVoidVoid> _onServiceInitialized;

        public static void Init()
        {
            Services = new Dictionary<Type, IBaseService>();
            _onServiceInitialized = new Dictionary<Type, DelegateHelper.DelegateVoidVoid>();
        }
        
        public static void Register<T>(T serviceInstance) where T : IBaseService
        {
            Register(serviceInstance, typeof(T));
        }
        
        public static void Register(IBaseService serviceInstance, Type type)
        {
            Services[type] = serviceInstance;
            serviceInstance.OnServiceFinishLoad += CheckForCallback;
        }

        private static void CheckForCallback()
        {
            List<Type> toDestroy = new List<Type>();
            foreach (var serviceInitializationCallback in _onServiceInitialized)
            {
                if (Get(serviceInitializationCallback.Key).IsLoaded)
                {
                    serviceInitializationCallback.Value?.Invoke();
                    toDestroy.Add(serviceInitializationCallback.Key);
                }
            }

            for (int i = toDestroy.Count - 1; i >= 0; i--)
            {
                _onServiceInitialized.Remove(toDestroy[i]);
            }
        }

        public static bool Exist<T>() => Exist(typeof(T));
        public static bool Exist(Type type)
        {
            return Services.ContainsKey(type);
        }

        public static T Get<T>() where T : IBaseService
        {
            return (T)Get(typeof(T));
        }
        
        public static IBaseService Get(Type type)
        {
            if (!Services.TryGetValue(type, out var newInstance))
            {
                //Debug.LogWarning($"Service of type {typeof(T)} not registered");
            }

            return newInstance;
        }

        public static void Reset()
        {
            Services.Clear();
        }

        public static void CallOnInitialized<T>(DelegateHelper.DelegateVoidVoid action) where T : IBaseService
        {
            var type = typeof(T);
            if(_onServiceInitialized.TryGetValue(type, out var actionEvent))
            {
                actionEvent += action;
                _onServiceInitialized[typeof(T)] = actionEvent;
            }
            else
            {
                _onServiceInitialized[typeof(T)] = action;
            }
        }
    }
}