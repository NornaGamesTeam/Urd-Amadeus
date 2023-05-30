using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Urd.Error;
using Urd.Services.Navigation;

namespace Urd.Services
{
    [Serializable]
    public class NavigationService : BaseService, INavigationService
    {
        private List<INavigable> _navigableOpened = new List<INavigable>();
        private List<INavigable> _navigableHistory = new List<INavigable>();

        [SerializeReference, SubclassSelector]
        private List<INavigationManager> _navigationManagers = new List<INavigationManager>();
        
        public event Action<INavigable> OnFinishLoadNavigable;

        public override void Init()
        {
            base.Init();

            LoadManagers();
        }

        private void LoadManagers()
        {
            for (int i = 0; i < _navigationManagers.Count; i++)
            {
                _navigationManagers[i].Init(CheckForInitialized);
            }
        }

        private void CheckForInitialized()
        {
            if (_navigationManagers.TrueForAll(manager => manager.IsInitialized))
            {
                SetAsLoaded();
            }
        }

        public TNavigable GetModel<TEnum, TNavigable>(TEnum enumValue, Action<bool> onOpenNavigableCallback) where TEnum : Enum where TNavigable : class, INavigable
        {
            var navigable = GetNavigationManager(enumValue);
            if (navigable != null)
            {
                return navigable.GetModel(enumValue) as TNavigable;
            }

            return null;
        }

        public void Open(INavigable navigable, Action<bool> onOpenNavigableCallback)
        {
            var navigationManager = GetNavigationManager(navigable);
            if (navigationManager == null)
            {
                var error = new ErrorModel(
                    $"[NavigationService] There no manager for the navigable {navigable}",
                    ErrorCode.Error_404_Not_Found,
                    UnityWebRequest.Result.DataProcessingError);
                Debug.LogWarning(error.ToString());

                onOpenNavigableCallback?.Invoke(false);
                return;
            }

            if (!navigationManager.CanOpen(navigable))
            {
                onOpenNavigableCallback?.Invoke(false);
                return;
            }

            navigationManager.Open(navigable,
                (success) => OnOpenNavigable(success, navigable, onOpenNavigableCallback));
        }


        private void OnOpenNavigable(bool success, INavigable navigable, Action<bool> onOpenNavigableCallback)
        {
            if (success)
            {
                _navigableOpened.Add(navigable);
                AddToHistory(navigable);
                OnFinishLoadNavigable?.Invoke(navigable);
            }

            onOpenNavigableCallback?.Invoke(success);
        }
        
        private INavigationManager GetNavigationManager<TEnum>(TEnum enumValue) where TEnum : Enum
        {
            for (int i = 0; i < _navigationManagers.Count; i++)
            {
                if (_navigationManagers[i].CanHandle(enumValue))
                {
                    return _navigationManagers[i];
                }
            }

            return null;
        }
        
        private INavigationManager GetNavigationManager(INavigable navigable)
        {
            for (int i = 0; i < _navigationManagers.Count; i++)
            {
                if (_navigationManagers[i].CanHandle(navigable))
                {
                    return _navigationManagers[i];
                }
            }

            return null;
        }

        public bool IsOpen(INavigable navigable)
        {
            return _navigableOpened.Exists(navigableOpened => navigableOpened.Id == navigable.Id);
        }

        private void AddToHistory(INavigable navigable)
        {
            _navigableHistory.Add(navigable);
        }

        public void Close(INavigable navigable, Action<bool> onCloseNavigableCallback = null)
        {
            if (navigable.IsClosingOrDestroyed)
            {
                return;
            }
            
            var navigationManager = GetNavigationManager(navigable);
            if (navigationManager == null)
            {
                var error = new ErrorModel(
                    $"[NavigationService] There no manager for the navigable {navigable}",
                    ErrorCode.Error_404_Not_Found,
                    UnityWebRequest.Result.DataProcessingError);
                Debug.LogWarning(error.ToString());
                onCloseNavigableCallback?.Invoke(false);
                
                return;
            }
            
            navigationManager.Close(navigable, (success) => OnCloseNavigable(success, navigable, onCloseNavigableCallback));
        }

        private void OnCloseNavigable(bool success, INavigable navigable, Action<bool> onCloseNavigable)
        {
            if (success)
            {
                _navigableOpened.Remove(navigable);
                navigable.ChangeStatus(NavigableStatus.Destroyed);
            }

            onCloseNavigable?.Invoke(success);
        }
    }
}