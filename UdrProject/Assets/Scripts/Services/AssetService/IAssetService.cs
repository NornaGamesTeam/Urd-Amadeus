using System;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Urd.Services
{
    public interface IAssetService : IBaseService
    {
        bool IsInitialized { get; }
        void LoadAsset<T>(string addressName, Action<T> assetCallback);
        void LoadScene(string sceneName, Action<SceneInstance> onLoadSceneCallback);
        void Instantiate(string addressName, Transform parent, Action<GameObject> instantiateCallback);
    }
}