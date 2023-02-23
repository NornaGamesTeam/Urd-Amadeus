using System;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using Urd.Scene;
using Urd.View.Popup;

namespace Urd.Services
{
    public interface IAssetService : IBaseService
    {
        void LoadAsset<T>(string addressName, Action<T> assetCallback);
        void LoadScene(SceneModel sceneModel, Action<SceneModel> onLoadSceneCallback);
        void UnLoadScene(SceneModel sceneModel, Action<bool> onUnloadSceneCallback);
        void Instantiate(string addressName, Transform parent, Action<GameObject> instantiateCallback);
        void Instantiate(GameObject prefab, Transform parent, Action<GameObject> instantiateCallback);
        void Instantiate<T>(T prefab, Transform parent, Action<T> instantiateCallback) where T : Behaviour;
        void Destroy(GameObject gameObject);
    }
}