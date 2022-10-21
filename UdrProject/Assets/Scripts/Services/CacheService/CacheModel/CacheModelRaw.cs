
using System;
using System.Diagnostics.Contracts;
using UnityEngine;
using Urd.Error;

namespace Urd.Services.Cache
{
    public class CacheModelRaw
    {
        public string Key { get; set; }
        public string RawValue { get; set; }
        public ErrorModel Error { get; set; }
        public bool IsSuccessSaved => Error != null;

        public CacheModelRaw(string key) : this(key, string.Empty) { }

        public CacheModelRaw(string key, string valueToSave)
        {
            Contract.Assert(key?.Length > 0, "[CacheModel] the key is null or empty");

            Key = key;
            RawValue = valueToSave;
        }

        public virtual bool TryGetValue<T>(out T castedValue) where T : class
        {
            castedValue = null;
            try
            {
                castedValue = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(RawValue);
                return true;
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"[CacheModel] Cannot casted {RawValue} to type {typeof(T)}. Error: {exception}");
                return false;
            }
        }

        public void SetError(ErrorModel error)
        {
            Error = error;
        }

        internal void SetLoadedValue(string textValue)
        {
            throw new NotImplementedException();
        }
    }
}