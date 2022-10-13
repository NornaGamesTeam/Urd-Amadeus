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

        public T Register<T>(T serviceInstance) where T : IBaseService
        {
            var registeredService = StaticServiceLocator.Register<T>(serviceInstance);
            registeredService.SetServiceLocatorService(this);
            serviceInstance.Init();
            return registeredService;
        }

        public void Reset()
        {
            StaticServiceLocator.Reset();
        }

    }
}