using System;
using UnityEngine;

namespace Urd.Services
{
    public interface IAssetService : IBaseService
    {
        void LoadLabel(string label);
        void LoadAsset<T>(string addressName, Action<T> assetCallback);
        void Instantiate(string addressName, Transform parent, Action<GameObject> instantiateCallback);
    }
}