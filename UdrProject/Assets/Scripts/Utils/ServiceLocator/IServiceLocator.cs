namespace Urd.Services
{
    public interface IServiceLocator : IBaseService
    {
        T Register<T>(T serviceInstance) where T : IBaseService;
        bool Exist<T>() where T : IBaseService;
        T Get<T>() where T : IBaseService;
        void Reset();
    }
}