using System;

namespace Urd.StartUp
{
    public interface IStartUpLoad
    {
        bool IsLoaded { get; }
        Type GetMainInterface();
        bool CanBeInitialized();
    }
}
