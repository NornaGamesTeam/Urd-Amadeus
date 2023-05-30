using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Urd.Error;
using Urd.Scene;
using Urd.Utils;

namespace Urd.Services.Navigation
{
    [Serializable]
    public class NavigationSceneManager : INavigationManager
    {
        private ServiceHelper<IAssetService> _assetService = new ServiceHelper<IAssetService>();

        private List<SceneModel> _scenesBeingOpened = new List<SceneModel>();
        private List<SceneModel> _scenesOpened = new List<SceneModel>();
            
        public bool IsInitialized { get; private set; }

        public NavigationSceneManager()
        {
        }

        public void Init(Action onInitialized)
        {
            IsInitialized = true;
            onInitialized?.Invoke();
        }

        public void Dispose()
        {
            
        }

        
        public TNavigable GetModel<TEnum, TNavigable>(TEnum enumValue) where TEnum : Enum where TNavigable : class, INavigable
        {
            if (Enum.TryParse(enumValue.ToString(), out SceneTypes sceneType))
            {
                return new SceneModel(sceneType) as TNavigable;
            }

            return null;
        }


        public bool CanHandle<TEnum>(TEnum enumValue) where TEnum : Enum
        {
            return typeof(TEnum) == typeof(SceneTypes);
        }
        
        public bool CanHandle(INavigable navigable)
        {
            return navigable is SceneModel;
        }

        public void Open(INavigable navigable, Action<bool> onOpenNavigable)
        {
            var sceneModel = navigable as SceneModel;

            if (!TryGetBuildSceneBuildIndex(sceneModel.Id, out int buildIndex))
            {
                var error = new ErrorModel(
                    $"[NavigationPopupManager] Error when try to get the scene, scene type {sceneModel.SceneType}",
                    ErrorCode.Error_404_Not_Found, UnityWebRequest.Result.DataProcessingError);
                Debug.LogWarning(error.ToString());

                onOpenNavigable?.Invoke(false);
                return;
            }

            sceneModel.SetBuildIndex(buildIndex);

            if (!_assetService.HasService)
            {
                _assetService.OnInitialize += () => LoadScene(sceneModel, onOpenNavigable);
                return;
            }

            LoadScene(sceneModel, onOpenNavigable);
        }

        private void LoadScene(SceneModel sceneModel, Action<bool> onOpenNavigable)
        {
            sceneModel.ChangeStatus(NavigableStatus.Opening);
            _scenesBeingOpened.Add(sceneModel);
            _assetService.Service.LoadScene(sceneModel, (sceneInstance) 
                => OnLoadScene(sceneModel, onOpenNavigable));
        }

        private bool TryGetBuildSceneBuildIndex(string sceneModelId, out int buildIndex)
        {
            buildIndex = 0;
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                var sceneTemp = SceneUtility.GetScenePathByBuildIndex(i);
                var sceneName = sceneTemp.Substring(sceneTemp.LastIndexOf("/")+1, sceneModelId.Length);
                if (sceneName == sceneModelId)
                {
                    buildIndex = i;
                    return true;
                }
            }
            return false;
        }

        private void OnLoadScene(SceneModel sceneModel, Action<bool> onOpenNavigable)
        {
            if (!sceneModel.HasScene)
            {
                var error = new ErrorModel(
                    $"[NavigationPopupManager] Error when try to load the scene, scene type {sceneModel.SceneType}",
                    ErrorCode.Error_404_Not_Found, UnityWebRequest.Result.DataProcessingError);
                Debug.LogWarning(error.ToString());
                
                onOpenNavigable?.Invoke(false);
                return;
            }
            
            _scenesBeingOpened.Remove(sceneModel);
            _scenesOpened.Add(sceneModel);
            sceneModel.ChangeStatus(NavigableStatus.Idle);
            onOpenNavigable?.Invoke(true);
        }

        public bool CanOpen(INavigable navigable)
        {
            return true;
        }

        public void Close(INavigable navigable, Action<bool> onCloseNavigable)
        {
            var sceneToClose = _scenesOpened.Find(
                scene => scene.Id == navigable.Id);
            if (sceneToClose == null)
            {
                var sceneBeingOpened = _scenesBeingOpened.Find(scene => scene.Id == navigable.Id);
                if (sceneBeingOpened == null)
                {
                    onCloseNavigable?.Invoke(false);
                    return;
                }
                sceneBeingOpened.OnStatusChanged += (statusBefore, statusAfter) => CloseSceneWhenOpen(navigable, statusBefore, statusAfter, onCloseNavigable);
                return;
            }
            
            sceneToClose.ChangeStatus(NavigableStatus.Closing);
            
            _assetService.Service.UnLoadScene(sceneToClose, success => OnSceneModelToCloseChangeStatus(success, sceneToClose, onCloseNavigable));
        }

        private void CloseSceneWhenOpen(INavigable navigable, NavigableStatus statusBefore, NavigableStatus statusAfter,
            Action<bool> onCloseNavigable)
        {
            if (statusAfter == NavigableStatus.Idle)
            {
                Close(navigable, onCloseNavigable);
            }
        }

        private void OnSceneModelToCloseChangeStatus(bool success, SceneModel sceneToClose,
            Action<bool> onCloseNavigable)
        {
            sceneToClose.ChangeStatus(NavigableStatus.Closed);
            _scenesOpened.Remove(sceneToClose);
            onCloseNavigable?.Invoke(success);
        }
    }
}