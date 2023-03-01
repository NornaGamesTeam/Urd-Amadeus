using System;

namespace Urd.Pool
{
    public interface IPoolable : IDisposable
    {
        void Init();
    }
}