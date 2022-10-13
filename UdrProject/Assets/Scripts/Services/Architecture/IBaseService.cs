namespace Urd.Services
{
    public interface IBaseService
    {
        IServiceLocator ServiceLocatorService { get; }
        void SetServiceLocatorService(IServiceLocator serviceLocatorService);

        void Init();
    }
}