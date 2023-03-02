
using System;
using System.Diagnostics.Contracts;
using UnityEngine;
using Urd.Error;

namespace Urd.Services.Cache
{
    public class CacheModel<T> : CacheModelRaw where T : class
    {
        public T Value { get; private set; }

        public CacheModel(string key) : base(key) { }
        public CacheModel(string key, T classToSerialize) : base (key, 
            Newtonsoft.Json.JsonConvert.SerializeObject(classToSerialize))
        {
            Value = classToSerialize;
        }
    }
}