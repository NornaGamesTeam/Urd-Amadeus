using System;
using Urd.StartUp;

namespace Urd.Services
{
    public interface IBaseService : IStartUpLoad
    {
        bool InitBegins { get; }
        IServiceLocator ServiceLocatorService { get; }
        void SetServiceLocatorService(IServiceLocator serviceLocatorService);
        void Init();
        event Action OnFinishLoad;
    }
}