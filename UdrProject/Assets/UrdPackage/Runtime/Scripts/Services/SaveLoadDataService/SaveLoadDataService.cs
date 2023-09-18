using System;
using System.Collections.Generic;
using MyBox;
using UnityEngine;
using Urd.Error;
using Urd.SaveLoad;

namespace Urd.Services
{ 
    [Serializable]
    public class SaveLoadDataService : BaseService, ISaveLoadDataService
    {
        private Dictionary<string, string> _keyValues = new Dictionary<string, string>();

        [field: SerializeReference, SubclassSelector] 
        private List<ISaveLoadDataServiceProvider> _providers;

        private Dictionary<string, int> _dataBeingSaved = new Dictionary<string, int>();
        private Dictionary<string, int> _dataBeingLoaded = new Dictionary<string, int>();

        public override void Init()
        {
            base.Init();
            
            SetAsLoaded();
        }

        public void AddProvider(ISaveLoadDataServiceProvider saveLoadDataServiceProvider)
        {
            if (_providers == null)
            {
                _providers = new();
            }
            _providers.Add(saveLoadDataServiceProvider);
        }
        
        public void SaveData<T>(string key, T value, Action<ErrorModel> onDataSavedCallback)
        {
            if (_providers.IsNullOrEmpty())
            {
                string message = "No Providers";
                onDataSavedCallback?.Invoke(new ErrorModel(message, ErrorCode.Error_204_No_Content));
                return;
            }
            
            for (int i = 0; i < _providers.Count; i++)
            {
                _providers[i].SaveData(key, value, (errorModel) => OnSaveData(key, errorModel, onDataSavedCallback) );
            }
        }

        private void OnSaveData(string key, ErrorModel errorModel, Action<ErrorModel> onDataSavedCallback)
        {
            if (!_dataBeingSaved.TryGetValue(key, out var calls))
            {
                if (!errorModel.IsSuccess)
                {
                    ErrorOnSaveData(key, errorModel, onDataSavedCallback);
                    return;
                }

                if (_providers.Count == 1)
                {
                    onDataSavedCallback?.Invoke(new ErrorModel());
                    return;
                }
                
                _dataBeingSaved.Add(key, 1);
                return;
            }


            if (calls < 0)
            {
                return;
            }

            if (!errorModel.IsSuccess)
            {
                ErrorOnSaveData(key, errorModel, onDataSavedCallback);
                return;
            }

            if (calls+1 < _providers.Count)
            {
                _dataBeingSaved[key]++;
                return;
            }

            _dataBeingSaved.Remove(key);
            onDataSavedCallback?.Invoke(new ErrorModel());
        }

        private void ErrorOnSaveData(string key, ErrorModel errorModel, Action<ErrorModel> onDataSavedCallback)
        {
            _dataBeingSaved.Add(key, -1);
            onDataSavedCallback?.Invoke(errorModel);
        }

        public void LoadData<T>(string key, T defaultValue, Action<ErrorModel, T> onDataLoadedCallback)
        {
            if (_providers.IsNullOrEmpty())
            {
                string message = "No Providers";
                onDataLoadedCallback?.Invoke(new ErrorModel(message, ErrorCode.Error_204_No_Content), defaultValue);
                return;
            }
            
            for (int i = 0; i < _providers.Count; i++)
            {
                _providers[i].LoadData(key, defaultValue, (errorModel, dataValue) => OnLoadData(key, dataValue, errorModel, onDataLoadedCallback) );
            }
        }

        private void OnLoadData<T>(string key, T dataValue, ErrorModel errorModel,
            Action<ErrorModel, T> onDataLoadedCallback)
        {
            if (!_dataBeingLoaded.TryGetValue(key, out var calls))
            {
                if (!errorModel.IsSuccess)
                {
                    ErrorOnLoadData(key, dataValue, errorModel, onDataLoadedCallback);
                    return;
                }

                if (_providers.Count == 1)
                {
                    onDataLoadedCallback?.Invoke(new ErrorModel(), dataValue);
                    return;
                }
                
                _dataBeingLoaded.Add(key, 1);
                return;
            }


            if (calls < 0)
            {
                return;
            }

            if (!errorModel.IsSuccess)
            {
                ErrorOnLoadData(key, dataValue, errorModel, onDataLoadedCallback);
                return;
            }

            if (calls+1 < _providers.Count)
            {
                _dataBeingSaved[key]++;
                return;
            }

            _dataBeingSaved.Remove(key);
            onDataLoadedCallback?.Invoke(new ErrorModel(), dataValue);
        }

        private void ErrorOnLoadData<T>(string key, T defaultValue, ErrorModel errorModel, Action<ErrorModel, T> onDataLoadedCallback)
        {
            _dataBeingSaved.Add(key, -1);
            onDataLoadedCallback?.Invoke(errorModel, defaultValue);
        }
    }
}