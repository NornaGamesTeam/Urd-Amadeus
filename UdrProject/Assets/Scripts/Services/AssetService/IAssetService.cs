using System;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using Urd.Scene;
using Urd.View.Popup;

namespace Urd.Services
{
    public interface IAssetService : IBaseService
    {
        bool IsInitialized { get; }
        void LoadAsset<T>(string addressName, Action<T> assetCallback);
        void LoadScene(SceneModel sceneModel, Action<SceneModel> onLoadSceneCallback);
        void UnLoadScene(SceneModel sceneModel, Action<bool> onUnloadSceneCallback);
        void Instantiate(string addressName, Transform parent, Action<GameObject> instantiateCallback);
        void Instantiate(GameObject prefab, Transform parent, Action<GameObject> instantiateCallback);
        void Destroy(GameObject gameObject);
    }
}