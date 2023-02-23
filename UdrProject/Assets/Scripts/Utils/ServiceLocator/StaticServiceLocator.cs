using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Services;

namespace Urd.Utils
{
    public static class StaticServiceLocator
    {
        private static Dictionary<Type, IBaseService>
            Services = new Dictionary<Type, IBaseService>();

        private static Dictionary<Type, DelegateHelper.DelegateVoidVoid> _onServiceRegistered
            = new Dictionary<Type, DelegateHelper.DelegateVoidVoid>();

        public static void Register<T>(T serviceInstance) where T : IBaseService
        {
            Register(serviceInstance, typeof(T));
        }
        
        public static void Register(IBaseService serviceInstance, Type type)
        {
            Services[type] = serviceInstance;
            
            if(_onServiceRegistered.TryGetValue(type, out var actionEvent))
            {
                actionEvent?.Invoke();
                _onServiceRegistered.Remove(type);
            }
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
                //Debug.LogWarning($"Service of type {typeof(T)} not registered");
            }

            return (T)newInstance;
        }

        public static void Reset()
        {
            Services.Clear();
        }

        public static void CallOnRegister<T>(DelegateHelper.DelegateVoidVoid action) where T : IBaseService
        {
            var type = typeof(T);
            if(_onServiceRegistered.TryGetValue(type, out var actionEvent))
            {
                actionEvent += action;
            }
            else
            {
                _onServiceRegistered[typeof(T)] = action;
            }
        }
    }
}