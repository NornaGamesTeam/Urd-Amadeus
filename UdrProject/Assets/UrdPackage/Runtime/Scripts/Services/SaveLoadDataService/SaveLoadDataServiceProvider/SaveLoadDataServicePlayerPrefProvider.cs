using System;
using Newtonsoft.Json;
using UnityEngine;
using Urd.Error;

namespace Urd.SaveLoad
{
    [Serializable]
    public class SaveLoadDataServicePlayerPrefProvider : ISaveLoadDataServiceProvider
    {
        public void SaveData<T>(string key, T value, Action<ErrorModel> onSaveDataCallback)
        {
            try
            {
                var valueJson = JsonConvert.SerializeObject(value);
                PlayerPrefs.SetString(key, valueJson);
                PlayerPrefs.Save();
                onSaveDataCallback?.Invoke(new ErrorModel());
                return;
            }
            catch (Exception exception)
            {
                onSaveDataCallback?.Invoke(new ErrorModel(exception));
            }
        }

        public void LoadData<T>(string key, T defaultValue, Action<ErrorModel, T> onLoadDataCallback)
        {
            if (!PlayerPrefs.HasKey(key))
            {
                string errorMessage = "No key saved";
                onLoadDataCallback?.Invoke(new ErrorModel(errorMessage, ErrorCode.Error_404_Not_Found), defaultValue);
                return;
            }

            try
            {
                var valueJson = PlayerPrefs.GetString(key, JsonConvert.SerializeObject(defaultValue));
                var valueDeserialized = JsonConvert.DeserializeObject<T>(valueJson);
                onLoadDataCallback?.Invoke(new ErrorModel(), valueDeserialized);
            }
            catch (Exception exception)
            {
                onLoadDataCallback?.Invoke(new ErrorModel(exception), defaultValue);
            }
        }
    }
}