using System;

namespace Urd.Services
{
    public interface IServiceLocator : IBaseService
    {
        void Register<T>(T serviceInstance) where T : IBaseService;
        void Register(IBaseService serviceInstance, Type type);
        bool Exist<T>() where T : IBaseService;
        T Get<T>() where T : IBaseService;
        void Reset();
    }
}