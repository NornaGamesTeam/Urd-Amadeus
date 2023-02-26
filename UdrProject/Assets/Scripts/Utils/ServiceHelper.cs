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

        public bool HasService => Service != null;
        public event DelegateHelper.DelegateVoidVoid OnInitialize
        {
            add
            {
                if (!HasService)
                {
                    StaticServiceLocator.CallOnRegister<T>(value);
                }
            }
            remove { }
        }

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
    }
}