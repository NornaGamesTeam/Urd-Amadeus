using System;
using Urd.Services;

namespace Urd.Services
{
    public interface IRemoteConfigurationService : IBaseService
    {
        void FetchData(Action onFetchData);
        bool TryGetDataAs<T>(string key, out T value);
    }
}