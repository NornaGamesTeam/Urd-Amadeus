using System;
using System.Collections.Generic;

namespace Urd.Services.RemoteConfiguration
{
    public interface IRemoteConfigurationProvider
    {
        delegate void OnGetRemoteConfigurationDataDelegate(Dictionary<string, string> keyValues);
        event OnGetRemoteConfigurationDataDelegate OnGetRemoteConfigurationData;

        void FetchData(Action onFetchData);
    }
}