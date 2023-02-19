using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Urd.Error;
using Urd.Popup;
using Urd.Utils;
using Urd.View.Popup;

namespace Urd.Services.Navigation
{
    public class NavigationPopupManager : INavigationManager
    {
        private const string POPUP_TYPES_CONFIG_PATH = "Navigation/PopupTypesConfig";
        
        private PopupTypesConfig _popupTypesConfig;
        private Transform _popupParent;
        
        private IAssetService _assetService;

        private List<PopupBodyView> _popupsOpened = new List<PopupBodyView>();
            
        public NavigationPopupManager()
        {
            _assetService = StaticServiceLocator.Get<IAssetService>();
            LoadPopupTypesConfig();
            LoadParent();
        }

        private void LoadParent()
        {
            _popupParent = GameObject.FindWithTag(CanvasTagNames.PopupCanvas.ToString())?.transform;
            if (_popupParent == null)
            {
                CreatePopupParent();
            }
        }

        private void CreatePopupParent()
        {
            _assetService.Instantiate(_popupTypesConfig.PopupCanvas.gameObject, null, 
                newPopupCanvas => _popupParent = newPopupCanvas.transform );
        }

        private void LoadPopupTypesConfig()
        {
            _popupTypesConfig = Resources.Load<PopupTypesConfig>(POPUP_TYPES_CONFIG_PATH);
            if (_popupTypesConfig == null)
            {
                var error = new ErrorModel(
                    $"[NavigationPopupManager] Error when try to load popup typs config in path {POPUP_TYPES_CONFIG_PATH}",
                    ErrorCode.Error_404_Not_Found, UnityWebRequest.Result.DataProcessingError);
                Debug.LogWarning(error.ToString());
            }
        }

        public bool CanHandle(INavigable navigable)
        {
            return navigable is PopupModel && _popupTypesConfig.Contains(navigable);
        }

        public void Open(INavigable navigable, Action<bool> onOpenNavigable)
        {
            var popupModel = navigable as PopupModel;

            if(!_popupTypesConfig.TryGetPopupView(popupModel, out var popupViewPrefab))
            {
                var error = new ErrorModel(
                    $"[NavigationPopupManager] Error when try to get the popup view from the config, popup type {popupModel.PopupType}",
                    ErrorCode.Error_404_Not_Found, UnityWebRequest.Result.DataProcessingError);
                Debug.LogWarning(error.ToString());
                
                onOpenNavigable?.Invoke(false);
                return;
            }
            
            _assetService.Instantiate(_popupTypesConfig.PopupBodyPrefab.gameObject, _popupParent,
                (popupBody) => OnInstantiatePopupBody(popupBody,popupViewPrefab, popupModel, onOpenNavigable));

        }

        private void OnInstantiatePopupBody(GameObject popupBodyGameObject, IPopupView popupViewPrefab, PopupModel popupModel,
            Action<bool> onOpenNavigable)
        {
            var popupBody = popupBodyGameObject.GetComponent<PopupBodyView>();
            _assetService.Instantiate(popupViewPrefab.GameObject, popupBody.Container, (popupView) => OnInstantiatePopupView(popupBody, popupView, popupModel, onOpenNavigable));
        }

        private void OnInstantiatePopupView(PopupBodyView popupBody, GameObject popupViewGameObject,
            PopupModel popupModel, Action<bool> onOpenNavigable)
        {
            if (popupViewGameObject == null)
            {
                var error = new ErrorModel(
                    $"[NavigationPopupManager] Error when try to instantiate the popup view, popup type {popupModel.PopupType}",
                    ErrorCode.Error_404_Not_Found, UnityWebRequest.Result.DataProcessingError);
                Debug.LogWarning(error.ToString());
                
                onOpenNavigable?.Invoke(false);
                return;
            }
            
            var popupView = popupViewGameObject.GetComponent<IPopupView>();
            popupView.SetPopupModel(popupModel);
            popupBody.SetPopupView(popupView);
            _popupsOpened.Add(popupBody);
            onOpenNavigable?.Invoke(true);
            
            popupBody.Open();
        }

        public bool CanOpen(INavigable navigable)
        {
            return true;
        }

        public void Close(INavigable navigable, Action<bool> onCloseNavigable)
        {
            var popupToClose = _popupsOpened.Find(
                popupBody => 
                    popupBody.PopupModel.Id == navigable.Id &&
                    !popupBody.PopupModel.IsClosingOrDestroyed);
            
            if (popupToClose == null)
            {
                onCloseNavigable?.Invoke(false);
                return;
            }
            
            popupToClose.PopupModel.OnStatusChanged += (statusFrom, statusTo) => OnPopupModelToCloseChangeStatus(popupToClose, statusFrom, statusTo, onCloseNavigable);
            
            popupToClose.Close();
        }

        private void OnPopupModelToCloseChangeStatus(PopupBodyView popupToClose, NavigableStatus statusFrom, NavigableStatus statusTo, Action<bool> onCloseNavigable)
        {
            if (statusTo == NavigableStatus.Closed)
            {
                _popupsOpened.Remove(popupToClose);
                _assetService.Destroy(popupToClose.gameObject);
                onCloseNavigable?.Invoke(true);
            }
        }
    }
}