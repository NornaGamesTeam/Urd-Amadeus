using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Urd.Error;
using Urd.Scene;
using Urd.Utils;

namespace Urd.Services.Navigation
{
    [Serializable]
    public class NavigationSceneManager : INavigationManager
    {
        private IAssetService _assetService;

        private List<SceneModel> _scenesOpened = new List<SceneModel>();
            
        public NavigationSceneManager()
        {
        }

        public void Init()
        {
            _assetService = StaticServiceLocator.Get<IAssetService>();
        }

        public bool CanHandle(INavigable navigable)
        {
            return navigable is SceneModel;
        }

        public void Open(INavigable navigable, Action<bool> onOpenNavigable)
        {
            var sceneModel = navigable as SceneModel;

            if(TryGetBuildSceneBuildIndex(sceneModel.Id, out int buildIndex))
            {
                sceneModel.SetBuildIndex(buildIndex);
            }
            
            _assetService.LoadScene(sceneModel, (sceneInstance) 
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
            
            _scenesOpened.Add(sceneModel);
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
                return;
            }
            
            sceneToClose.ChangeStatus(NavigableStatus.Closing);
            
            _assetService.UnLoadScene(sceneToClose, success => OnSceneModelToCloseChangeStatus(success, sceneToClose, onCloseNavigable));
        }

        private void OnSceneModelToCloseChangeStatus(bool success, SceneModel sceneToClose,
            Action<bool> onCloseNavigable)
        {
            _scenesOpened.Remove(sceneToClose);
            onCloseNavigable?.Invoke(success);
        }
    }
}