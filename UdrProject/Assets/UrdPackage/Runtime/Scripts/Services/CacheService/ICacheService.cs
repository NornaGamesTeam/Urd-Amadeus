using System;
using UnityEngine;
using Urd.Error;
using Urd.Services.Cache;

namespace Urd.Services
{
    public interface ICacheService : IBaseService
    {
        void SaveCache(CacheModelRaw cacheModel, Action<CacheModelRaw> onCacheSavedSuccess, Action<ErrorModel> onCacheSavedFailed);
        void LoadCache(CacheModelRaw cacheModel, Action<CacheModelRaw> onCacheLoadedSuccess, Action<ErrorModel> onCacheLoadedFailed);
    }
}