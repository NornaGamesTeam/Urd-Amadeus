using System;
using System.Collections.Generic;
using PlasticGui.WebApi.Responses;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Urd.Scene;

namespace Urd.Services
{
    public class AssetService : BaseService, IAssetService
    {
        private IResourceLocator _resourceLocator;
        
        public bool IsInitialized { get; private set; }

        public event Action _callWhenInit;
        
        public override void Init()
        {
            base.Init();

            Addressables.InitializeAsync().Completed += OnAddressableInitialize;
        }

        private void OnAddressableInitialize(AsyncOperationHandle<IResourceLocator> resourceLocator)
        {
            _resourceLocator = resourceLocator.Result;
            
            UnityEngine.Debug.Log($"[AssetService] OnAddressableInitialize {resourceLocator.Status}");

            IsInitialized = true;
            
            _callWhenInit?.Invoke();
            _callWhenInit = null;
        }

        public void LoadAsset<T>(string addressName, Action<T> assetCallback)
        {
            _resourceLocator.Locate(addressName, typeof(T), out var location);

            if (location != null && location.Count > 0)
            {
                if (location.Count > 1)
                {
                    Debug.LogWarning($"[AssetService] LoadAsset {addressName}, more than 1 asset with this name");
                }
                LoadAssetIntenal<T>(location[0], assetCallback);
            }

            LoadAssetIntenal<T>(addressName, assetCallback);
        }

        private void LoadAssetIntenal<T>(IResourceLocation resourceLocation, Action<T> assetCallback)
        {
            Addressables.LoadAssetAsync<T>(resourceLocation).Completed += 
                task => OnLoadAsset<T>(task, resourceLocation.PrimaryKey, assetCallback);
        }

        private void LoadAssetIntenal<T>(string addressName, Action<T> assetCallback)
        {
            Addressables.LoadAssetAsync<T>(addressName).Completed += (task) => OnLoadAsset<T>(task, addressName, assetCallback);
        }

        private void OnLoadAsset<T>(AsyncOperationHandle<T> task, string addressableName, Action<T> assetCallback)
        {
            if (task.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogWarning($"[AssetService] OnLoadAsset {addressableName} cannot Instantiate");
                assetCallback?.Invoke(default(T));
                return;
            }
            assetCallback?.Invoke(task.Result);
        }

        public void LoadScene(SceneModel sceneModel, Action<SceneModel> onLoadSceneCallback)
        {
            if (sceneModel.IsInBuildIndex)
            {
                LoadSceneFromBuildIndex(sceneModel, onLoadSceneCallback);
            }
            else
            {
                LoadSceneFromAddressable(sceneModel, onLoadSceneCallback);
            }
        }

        private void LoadSceneFromBuildIndex(SceneModel sceneModel, Action<SceneModel> onLoadSceneCallback)
        {
            SceneManager.LoadSceneAsync(sceneModel.BuildIndex, LoadSceneMode.Additive).completed += 
                task => OnLoadSceneFromBuildIndex(task, sceneModel, onLoadSceneCallback); 
        }

        private void OnLoadSceneFromBuildIndex(AsyncOperation task, SceneModel sceneModel,
            Action<SceneModel> onLoadSceneCallback)
        {
            if (!task.isDone)
            {
                Debug.LogWarning($"[AssetService] OnLoadSceneFromBuildIndex {sceneModel.Id} cannot Instantiate");
                onLoadSceneCallback?.Invoke(sceneModel);
                return;
            }

            var scene = SceneManager.GetSceneByBuildIndex(sceneModel.BuildIndex);
            sceneModel.SetScene(scene);
            onLoadSceneCallback.Invoke(sceneModel);
        }

        private void LoadSceneFromAddressable(SceneModel sceneModel, Action<SceneModel> onLoadSceneCallback)
        {
            _resourceLocator.Locate(sceneModel, typeof(SceneInstance), out var location);

            if (location != null && location.Count > 0)
            {
                if (location.Count > 1)
                {
                    Debug.LogWarning($"[AssetService] LoadSceneFromAddressable {sceneModel}, more than 1 asset with this name");
                }
                LoadSceneInternal(sceneModel, location[0], onLoadSceneCallback);
            }

            LoadSceneInternal(sceneModel, onLoadSceneCallback);
        }
        
        private void LoadSceneInternal(SceneModel sceneModel, IResourceLocation resourceLocation,
            Action<SceneModel> onLoadSceneCallback)
        {
            Addressables.LoadSceneAsync(resourceLocation, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += 
                task => OnLoadSceneFromAddressable(task, sceneModel, onLoadSceneCallback);
        }

        private void LoadSceneInternal(SceneModel sceneModel, Action<SceneModel> onLoadSceneCallback)
        {
            Addressables.LoadSceneAsync(sceneModel.Id, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += 
                task => OnLoadSceneFromAddressable(task, sceneModel, onLoadSceneCallback);
        }

        private void OnLoadSceneFromAddressable(AsyncOperationHandle<SceneInstance> task, SceneModel sceneModel, Action<SceneModel> onLoadSceneCallback)
        {
            if (task.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogWarning($"[AssetService] OnLoadSceneFromAddressable {sceneModel.Id} cannot Instantiate");
                onLoadSceneCallback?.Invoke(sceneModel);
                return;
            }
            sceneModel.SetSceneInstance(task.Result);
            onLoadSceneCallback?.Invoke(sceneModel);
        }

        public void UnLoadScene(SceneModel sceneModel, Action<bool> onUnloadSceneCallback)
        {
            if (!sceneModel.HasScene)
            {
                Debug.LogWarning($"[AssetService] UnLoadScene {sceneModel.Id} doesn't have a scene");
                onUnloadSceneCallback.Invoke(false);
                return;
            }

            if (sceneModel.Scene.IsValid())
            {
                UnLoadSceneFromBuildIndex(sceneModel, onUnloadSceneCallback);
            }
            else
            {
                UnloadSceneFromAddressable(sceneModel, onUnloadSceneCallback);
            }
        }

        private void UnLoadSceneFromBuildIndex(SceneModel sceneModel, Action<bool> onUnloadSceneCallback)
        {
            SceneManager.UnloadSceneAsync(sceneModel.Scene).completed += 
                task => OnUnLoadSceneFromBuildIndex(task, sceneModel, onUnloadSceneCallback);
        }

        private void OnUnLoadSceneFromBuildIndex(AsyncOperation task, SceneModel sceneModel, Action<bool> onUnloadSceneCallback)
        {
            if (!task.isDone)
            {
                Debug.LogWarning($"[AssetService] OnUnLoadSceneFromBuildIndex {sceneModel.Id} cannot be unloaded");
                onUnloadSceneCallback?.Invoke(false);
                return;
            }

            sceneModel.CleanScene();
            onUnloadSceneCallback.Invoke(true);
        }


        private void UnloadSceneFromAddressable(SceneModel sceneModel, Action<bool> onLoadSceneCallback)
        {
            Addressables.UnloadSceneAsync(sceneModel.SceneInstance).Completed += 
                task => OnUnloadSceneFromAddressable(task, sceneModel, onLoadSceneCallback);
        }

        private void OnUnloadSceneFromAddressable(AsyncOperationHandle<SceneInstance> task, SceneModel sceneModel, Action<bool> onLoadSceneCallback)
        {
            if (task.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogWarning($"[AssetService] OnLoadScene {sceneModel.SceneInstance.Scene.name} cannot unload");
                onLoadSceneCallback?.Invoke(false);
                return;
            }
            sceneModel.CleanScene();
            onLoadSceneCallback?.Invoke(true);
        }

        public void Instantiate(string addressName, Transform parent, Action<GameObject> instantiateCallback)
        {
            if (!IsInitialized)
            {
                CallWhenInit(() => Instantiate(addressName, parent, instantiateCallback));
                return;
            }
            
            _resourceLocator.Locate(addressName, typeof(GameObject), out var location);

            if(location != null && location.Count > 0)
            {
                if(location.Count > 1)
                {
                    Debug.LogWarning($"[AssetService] Instantiate {addressName}, more than 1 asset with this name");
                }
                InstantiateInternal(location[0], parent, instantiateCallback);
                return;
            }

            InstantiateInternal(addressName, parent, instantiateCallback);
        }

        private void CallWhenInit(Action action)
        {
            _callWhenInit += action;
        }

        public void Instantiate(GameObject prefab, Transform parent, Action<GameObject> instantiateCallback)
        {
            var newGameObject = GameObject.Instantiate(prefab, parent);
            instantiateCallback.Invoke(newGameObject);
        }
        
        public void Instantiate<T>(T prefab, Transform parent, Action<T> instantiateCallback) where T : Behaviour
        {
            var newGameObject = GameObject.Instantiate<T>(prefab, parent);
            instantiateCallback.Invoke(newGameObject);
        }

        public void Destroy(GameObject gameObject)
        {
            GameObject.Destroy(gameObject);
        }

        private void InstantiateInternal(string addressName, Transform parent, Action<GameObject> instantiateCallback)
        {
            Addressables.InstantiateAsync(addressName, parent).Completed += (task) 
                => OnInstantiate(task, addressName, instantiateCallback);
        }

        private void InstantiateInternal(IResourceLocation resourceLocation, Transform parent, Action<GameObject> instantiateCallback)
        {
            Addressables.InstantiateAsync(resourceLocation, parent).Completed += (task) 
                => OnInstantiate(task, resourceLocation.InternalId, instantiateCallback);
        }

        private void OnInstantiate(AsyncOperationHandle<GameObject> task, string addressableName, Action<GameObject> instantiateCallback)
        {
            if (task.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogWarning($"[AssetService] OnInstantiate {addressableName} cannot Instantiate");
                instantiateCallback?.Invoke(null);
                return;
            }
            instantiateCallback?.Invoke(task.Result);
        }
    }
}