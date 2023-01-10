using System;
using NSubstitute.Extensions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Urd.Error;
using Urd.Popup;
using Urd.Utils;
using Urd.View.Popup;

namespace Urd.Services.Navigation
{
    public class NavigationPopupManager : INavigationManager
    {
        private const string POPUP_TYPES_CONFIG_PATH = "PopupTypesConfig";
        
        private PopupTypesConfig _popupTypesConfig;
        private Transform _popupParent;
        
        private IAssetService _assetService;
        
        public NavigationPopupManager()
        {
            _assetService = StaticServiceLocator.Get<IAssetService>();
            LoadParent();
            LoadPopupTypesConfig();
        }

        private void LoadParent()
        {
            _popupParent = GameObject.FindWithTag(CanvasNames.PopupCanvas.ToString())?.transform;
            if (_popupParent == null)
            {
                var error = new ErrorModel(
                    $"[NavigationPopupManager] Error when try to get the popup Canvas with Tag {CanvasNames.PopupCanvas}",
                    ErrorCode.Error_404_Not_Found, UnityWebRequest.Result.DataProcessingError);
                Debug.LogWarning(error.ToString());
            }
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
            
            _assetService.Instantiate(popupViewPrefab, _popupParent, (popupView) => OnInstantiatePopup(popupView, popupModel, onOpenNavigable));
            
            onOpenNavigable?.Invoke(true);
        }

        private void OnInstantiatePopup(GameObject popupViewGameObject, PopupModel popupModel, Action<bool> onOpenNavigable)
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
        }

        public bool CanOpen(INavigable navigable)
        {
            return true;
        }

        public void Close(INavigable navigable, Action<bool> onCloseNavigable)
        {
            onCloseNavigable?.Invoke(true);
        }
    }
}