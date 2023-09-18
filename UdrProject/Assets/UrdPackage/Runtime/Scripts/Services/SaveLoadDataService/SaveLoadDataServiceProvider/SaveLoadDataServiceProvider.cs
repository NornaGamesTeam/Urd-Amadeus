using System;
using Urd.Error;

namespace Urd.SaveLoad
{
    
    public interface ISaveLoadDataServiceProvider
    {
        void SaveData<T>(string key, T value, Action<ErrorModel> onSaveDataCallback);
        void LoadData<T>(string key, T defaultValue, Action<ErrorModel, T> onLoadDataCallback);
    }
}
