using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Urd.Error;
using Urd.Utils;
using Urd.View.WorldAreaTransition;
using Urd.WorldAreaTransition;

namespace Urd.Services.Navigation
{
    [Serializable]
    public class WorldAreaTransitionManager : INavigationManager
    {
        private const string WORLD_AREA_TYPES_CONFIG_PATH = "Navigation/WorldAreaTransitionTypesConfig";
        
        private WorldAreaTransitionTypesConfig _worldAreaTransitionTypesConfig;
        private Transform _worldAreaTransitionParent;

        private ServiceHelper<IAssetService> _assetService = new ServiceHelper<IAssetService>();

        private List<IWorldAreaTransitionView> worldAreaTransitionViewsOpened = new List<IWorldAreaTransitionView>();
            
        public bool IsInitialized { get; private set; }
        
        public WorldAreaTransitionManager()
        {
        }

        public void Init(Action onInitialized)
        {
            LoadPopupTypesConfig();
            LoadParent();
            IsInitialized = true;
            onInitialized?.Invoke();
        }
        
        public void Dispose()
        {
            
        }
        
        public TNavigable GetModel<TEnum, TNavigable>(TEnum enumValue) where TEnum : Enum where TNavigable : class, INavigable
        {
            if (Enum.TryParse(enumValue.ToString(), out WorldAreaTransitionTypes popupType))
            {
                return new WorldAreaTransitionModel(popupType) as TNavigable;
            }

            return null;
        }

        public bool CanHandle<TEnum>(TEnum enumValue) where TEnum : Enum
        {
            return typeof(TEnum) == typeof(WorldAreaTransitionTypes);
        }
        
        private void LoadParent()
        {
            _worldAreaTransitionParent = GameObject.FindWithTag(CanvasTagNames.WorldAreaTransitionCanvas.ToString())?.transform;
            if (_worldAreaTransitionParent == null)
            {
                CreatePopupParent();
            }
            else
            {
                Debug.LogWarning($"Game Object with tag {CanvasTagNames.WorldAreaTransitionCanvas} not found");
            }
        }

        private void CreatePopupParent()
        {
            if (!_assetService.HasService)
            {
                _assetService.OnInitialize += LoadParent;
                return;
            }
            
            _assetService.Service.Instantiate(_worldAreaTransitionTypesConfig.WorldAreaCanvas.gameObject, null, 
                newPopupCanvas => _worldAreaTransitionParent = newPopupCanvas.transform );
        }

        private void LoadPopupTypesConfig()
        {
            _worldAreaTransitionTypesConfig = Resources.Load<WorldAreaTransitionTypesConfig>(WORLD_AREA_TYPES_CONFIG_PATH);
            if (_worldAreaTransitionTypesConfig == null)
            {
                var error = new ErrorModel(
                    $"[NavigationWorldAreaTransitionManager] Error when try to load world Area Transition types config in path {WORLD_AREA_TYPES_CONFIG_PATH}",
                    ErrorCode.Error_404_Not_Found, UnityWebRequest.Result.DataProcessingError);
                Debug.LogWarning(error.ToString());
            }
        }

        public bool CanHandle(INavigable navigable)
        {
            return navigable is WorldAreaTransitionModel && _worldAreaTransitionTypesConfig.Contains(navigable);
        }

        public void Open(INavigable navigable, Action<bool> onOpenNavigable)
        {
            var worldAreaTransitionModel = navigable as WorldAreaTransitionModel;

            if(!_worldAreaTransitionTypesConfig.TryGetWorldAreaTransitionView(worldAreaTransitionModel, out var worldAreaTransitionViewPrefab))
            {
                var error = new ErrorModel(
                    $"[NavigationWorldAreaTransitionManager] Error when try to get the popup view from the config, popup type {worldAreaTransitionModel.WorldAreaTransitionType}",
                    ErrorCode.Error_404_Not_Found, UnityWebRequest.Result.DataProcessingError);
                Debug.LogWarning(error.ToString());
                
                onOpenNavigable?.Invoke(false);
                return;
            }
            
            _assetService.Service.Instantiate(
                worldAreaTransitionViewPrefab.GameObject, 
                _worldAreaTransitionParent, 
                (worldAreaTransitionView) 
                    => OnInstantiateWorldAreaTransitionView(worldAreaTransitionView, worldAreaTransitionModel, onOpenNavigable));
        }

        private void OnInstantiateWorldAreaTransitionView(
            GameObject popupViewGameObject, 
            WorldAreaTransitionModel worldAreaTransitionModel, 
            Action<bool> onOpenNavigable)
        {
            if (popupViewGameObject == null)
            {
                var error = new ErrorModel(
                    $"[NavigationWorldAreaTransitionManager] Error when try to instantiate the popup view, popup type {worldAreaTransitionModel.WorldAreaTransitionType}",
                    ErrorCode.Error_404_Not_Found, UnityWebRequest.Result.DataProcessingError);
                Debug.LogWarning(error.ToString());
                
                onOpenNavigable?.Invoke(false);
                return;
            }
            
            var worldAreaTransitionView = popupViewGameObject.GetComponent<IWorldAreaTransitionView>();
            worldAreaTransitionView.SetWorldAreaTransitionModel(worldAreaTransitionModel);
            worldAreaTransitionViewsOpened.Add(worldAreaTransitionView);
            onOpenNavigable?.Invoke(true);
        }

        public bool CanOpen(INavigable navigable)
        {
            return true;
        }

        public void Close(INavigable navigable, Action<bool> onCloseNavigable)
        {
            var areaTransitionViewToClose = worldAreaTransitionViewsOpened.Find(
                worldAreaTransitionView => 
                    worldAreaTransitionView.WorldAreaTransitionModel.Id == navigable.Id &&
                    !worldAreaTransitionView.WorldAreaTransitionModel.IsClosingOrDestroyed);
            
            if (areaTransitionViewToClose == null)
            {
                onCloseNavigable?.Invoke(false);
                return;
            }
            
            areaTransitionViewToClose.WorldAreaTransitionModel.OnStatusChanged += (statusFrom, statusTo) => OnPopupModelToCloseChangeStatus(areaTransitionViewToClose, statusFrom, statusTo, onCloseNavigable);
            
            areaTransitionViewToClose.Close();
        }

        private void OnPopupModelToCloseChangeStatus(IWorldAreaTransitionView worldAreaTransitionViewToClose, NavigableStatus statusFrom, NavigableStatus statusTo, Action<bool> onCloseNavigable)
        {
            if (statusTo == NavigableStatus.Closed)
            {
                worldAreaTransitionViewsOpened.Remove(worldAreaTransitionViewToClose);
                _assetService.Service.Destroy(worldAreaTransitionViewToClose.GameObject);
                onCloseNavigable?.Invoke(true);
            }
        }
    }
}