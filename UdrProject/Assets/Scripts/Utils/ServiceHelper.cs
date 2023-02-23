using System;
using UnityEngine;
using Urd.Services;

namespace Urd.Utils
{
    public class ServiceHelper<T> where T : IBaseService
    {
        public T Service
        {
            get
            {
                if (_service == null)
                {
                    LoadService();
                }

                return _service;
            }
        }

        private T _service;

        public ServiceHelper(bool loadOnAwake = false)
        {
            if (loadOnAwake)
            {
                LoadService();
            }
        }

        private void LoadService()
        {
            _service = StaticServiceLocator.Get<T>();
        }

        public void OnRegister(DelegateHelper.DelegateVoidVoid action)
        {
            StaticServiceLocator.CallOnRegister<T>(action);
        }
    }
}