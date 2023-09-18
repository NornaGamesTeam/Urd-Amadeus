using System;
using Urd.Error;
using Urd.SaveLoad;

namespace Urd.Services
{
    public interface ISaveLoadDataService : IBaseService
    {
        public void AddProvider(ISaveLoadDataServiceProvider saveLoadDataServiceProvider);
        void SaveData<T>(string key, T value, Action<ErrorModel> onDataSavedCallback);
        void LoadData<T>(string key, T defaultValue, Action<ErrorModel, T> onDataLoadedCallback);
    }
}