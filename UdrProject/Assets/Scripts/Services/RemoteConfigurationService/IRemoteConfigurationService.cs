using System;
using Urd.Services;
using Urd.Services.RemoteConfiguration;

namespace Urd.Services
{
    public interface IRemoteConfigurationService : IBaseService
    {
        RemoteConfigurationEnvironmentType Environment { get; }
        void FetchData(Action onFetchData);
        bool TryGetDataAs<T>(string key, out T value);
    }
}