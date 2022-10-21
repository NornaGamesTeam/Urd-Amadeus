using System;
using System.IO;
using UnityEngine;
using Urd.Error;
using Urd.Services.Cache;

namespace Urd.Services
{
    public class CacheService : BaseService, ICacheService
    {
        private const string PATH = "{0}/{1}";

        public void SaveCache(CacheModelRaw cacheModel, Action<CacheModelRaw> onCacheSavedSuccess, Action<ErrorModel> onCacheSavedFailed)
        {
            SaveCacheInternal(cacheModel, onCacheSavedSuccess, onCacheSavedFailed);
        }

        private void SaveCacheInternal(CacheModelRaw cacheModel, Action<CacheModelRaw> onCacheSavedSuccess, Action<ErrorModel> onCacheSavedFailed)
        {
            string fullPath = GetPath(cacheModel);

            try
            {
                File.WriteAllText(fullPath, cacheModel.RawValue);
                onCacheSavedSuccess?.Invoke(cacheModel);
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"[CacheService] Error while try to save cache in path {cacheModel.Key} with value {cacheModel.RawValue}. Error: {exception}");
                var errorModel = new ErrorModel(exception, ErrorCode.Error_700_File_Save_Error);
                cacheModel.SetError(errorModel);
                onCacheSavedFailed?.Invoke(errorModel);
            }
        }

        private string GetPath(CacheModelRaw cacheModel)
        {
            return string.Format(PATH, Application.persistentDataPath, cacheModel.Key);
        }

        public void LoadCache(CacheModelRaw cacheModel, Action<CacheModelRaw> onCacheLoadedSuccess, Action<ErrorModel> onCacheLoadedFailed)
        {
            string fullPath = GetPath(cacheModel);

            try
            {
                string textValue = File.ReadAllText(fullPath);
               
                cacheModel.SetLoadedValue(textValue);
                onCacheLoadedSuccess?.Invoke(cacheModel);
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"[CacheService] Error while try to save cache in path {cacheModel.Key} with value {cacheModel.RawValue}. Error: {exception}");
                var errorModel = new ErrorModel(exception, ErrorCode.Error_701_File_Load_Error);
                cacheModel.SetError(errorModel);
                onCacheLoadedFailed?.Invoke(errorModel);
            }
        }
    }
}