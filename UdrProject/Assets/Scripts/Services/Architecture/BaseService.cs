namespace Urd.Services
{
    public class BaseService : IBaseService
    {
        public virtual void Init()
        {

        }
        public IServiceLocator ServiceLocatorService { get; private set; }

        public void SetServiceLocatorService(IServiceLocator serviceLocatorService)
        {
            ServiceLocatorService = serviceLocatorService;
        }
    }
}