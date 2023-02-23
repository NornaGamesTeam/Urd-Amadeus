using System;

namespace Urd.Services
{
    public interface IStartUpService : IBaseService
    {
        float LoadingFactor { get; }
        public event Action<float> OnLoadingFactorChanged;
    }
}
