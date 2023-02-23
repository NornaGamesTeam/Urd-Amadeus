using System;
using Urd.StartUp;

namespace Urd.Services
{
    public interface IBaseService : IStartUpLoad
    {
        IServiceLocator ServiceLocatorService { get; }
        void SetServiceLocatorService(IServiceLocator serviceLocatorService);
        void Init();
        event Action OnFinishLoad;
    }
}