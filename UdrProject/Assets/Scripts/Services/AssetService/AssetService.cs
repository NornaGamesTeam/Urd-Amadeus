using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Urd.Services
{
    public class AssetService : BaseService, IAssetService
    {
        private IResourceLocator _resourceLocator;

        public override void Init()
        {
            base.Init();

            Addressables.InitializeAsync().Completed += OnAddressableInitialize;
        }

        private void OnAddressableInitialize(AsyncOperationHandle<IResourceLocator> resourceLocator)
        {
            UnityEngine.Debug.Log($"[AssetService] OnAddressableInitialize {resourceLocator.Status}");

            _resourceLocator = resourceLocator.Result;
        }
        
        public void LoadLabel(string label)
        {
            Addressables.LoadContentCatalogAsync(label).Completed += OnLoadLabel;
        }

        private void OnLoadLabel(AsyncOperationHandle<IResourceLocator> labelResources)
        {
            UnityEngine.Debug.Log($"[AssetService] OnLoadLabel {labelResources}");
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
            Addressables.LoadAssetAsync<T>(resourceLocation).Completed += (task) => OnLoadAsset<T>(task, resourceLocation.PrimaryKey, assetCallback);
        }

        private void LoadAssetIntenal<T>(string addressName, Action<T> assetCallback)
        {
            Addressables.LoadAssetAsync<T>(addressName).Completed += (task) => OnLoadAsset<T>(task, addressName, assetCallback);
        }

        private void OnLoadAsset<T>(AsyncOperationHandle<T> task, string addressableName, Action<T> assetCallback)
        {
            if (task.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogWarning($"[AssetService] OnInstantiate {addressableName} cannot Instantiate");
                assetCallback?.Invoke(default(T));
                return;
            }
            assetCallback?.Invoke(task.Result);
        }

        public void LoadScene(string sceneName, Action<SceneInstance> onLoadSceneCallback)
        {
            _resourceLocator.Locate(sceneName, typeof(SceneInstance), out var location);

            if (location != null && location.Count > 0)
            {
                if (location.Count > 1)
                {
                    Debug.LogWarning($"[AssetService] LoadScene {sceneName}, more than 1 asset with this name");
                }
                LoadSceneInternal(location[0], onLoadSceneCallback);
            }

            LoadSceneInternal(sceneName, onLoadSceneCallback);
        }

        private void LoadSceneInternal(IResourceLocation resourceLocation, Action<SceneInstance> onLoadSceneCallback)
        {
            Addressables.LoadSceneAsync(resourceLocation, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += (task) => OnLoadScene(task, resourceLocation.PrimaryKey, onLoadSceneCallback);
        }

        private void LoadSceneInternal(string sceneName, Action<SceneInstance> onLoadSceneCallback)
        {
            Addressables.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += (task) => OnLoadScene(task, sceneName, onLoadSceneCallback);
        }

        private void OnLoadScene(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> task, string sceneName, Action<SceneInstance> onLoadSceneCallback)
        {
            if (task.Status == AsyncOperationStatus.Failed)
            {
                Debug.LogWarning($"[AssetService] OnLoadScene {sceneName} cannot Instantiate");
                onLoadSceneCallback?.Invoke(default(SceneInstance));
                return;
            }
            onLoadSceneCallback?.Invoke(task.Result);
        }

        public void Instantiate(string addressName, Transform parent, Action<GameObject> instantiateCallback)
        {
            _resourceLocator.Locate(addressName, typeof(GameObject), out var location);

            if(location != null && location.Count > 0)
            {
                if(location.Count > 1)
                {
                    Debug.LogWarning($"[AssetService] Instantiate {addressName}, more than 1 asset with this name");
                }
                InstantiateInternal(location[0], parent, instantiateCallback);
            }

            InstantiateInternal(addressName, parent, instantiateCallback);
        }

        private void InstantiateInternal(string addressName, Transform parent, Action<GameObject> instantiateCallback)
        {
            Addressables.InstantiateAsync(addressName, parent).Completed += (task) => OnInstantiate(task, addressName, instantiateCallback);
        }

        private void InstantiateInternal(IResourceLocation resourceLocation, Transform parent, Action<GameObject> instantiateCallback)
        {
            Addressables.InstantiateAsync(resourceLocation, parent).Completed += (task) => OnInstantiate(task, resourceLocation.InternalId, instantiateCallback);
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