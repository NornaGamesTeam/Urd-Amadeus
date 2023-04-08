using System;
using Urd.Utils;

namespace Urd.Services
{
    [Serializable]
    public class ServiceLocator : BaseService, IServiceLocator
    {
        public ServiceLocator()
        {
            StaticServiceLocator.Init();
            Register<IServiceLocator>(this);
        }

        public override void Init()
        {
            base.Init();
            SetAsLoaded();
        }

        public bool Exist<T>() where T : IBaseService
        {
            return StaticServiceLocator.Exist<T>();
        }

        public T Get<T>() where T : IBaseService
        {
            return StaticServiceLocator.Get<T>();
        }

        public void Register<T>(T serviceInstance) where T : IBaseService
        {
            Register(serviceInstance, typeof(T));
        }
        public void Register(IBaseService serviceInstance, Type type)
        {
            serviceInstance.SetServiceLocatorService(this);
            serviceInstance.Init();
            StaticServiceLocator.Register(serviceInstance, type);
        }

        public void Reset()
        {
            StaticServiceLocator.Reset();
        }

    }
}