using System;
using Urd.Utils;

namespace Urd.Services
{
    public class ServiceLocator : BaseService, IServiceLocator
    {
        public ServiceLocator()
        {
            StaticServiceLocator.Register<IServiceLocator>(this);
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
            StaticServiceLocator.Register(serviceInstance, type);
            serviceInstance.SetServiceLocatorService(this);
            serviceInstance.Init();
        }

        public void Reset()
        {
            StaticServiceLocator.Reset();
        }

    }
}