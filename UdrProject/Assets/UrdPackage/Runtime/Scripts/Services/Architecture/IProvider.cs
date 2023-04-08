using System;

namespace Urd.Services
{
    public interface IProvider : IDisposable
    {
        void Init();
    }
}
