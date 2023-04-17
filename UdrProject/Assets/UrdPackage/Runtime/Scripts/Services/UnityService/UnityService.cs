using System;
using UnityEngine;
using Urd.Services.Unity;

namespace Urd.Services
{
    [Serializable]
    public class UnityService : BaseService, IUnityService
    {
        private static string UNITY_VIEW_GAMEOBJECT_NAME = "UnityServiceView";

        private UnityServiceView _unityServiceView;

        private IClockService _clockService;
        private IEventBusService _eventBusService;

        public override void Init()
        {
            base.Init();

            _clockService = ServiceLocatorService.Get<IClockService>();
            _eventBusService = ServiceLocatorService.Get<IEventBusService>();

            CreateCoroutineView();
            SetAsLoaded();
        }

        private void CreateCoroutineView()
        {
            _unityServiceView = new GameObject(UNITY_VIEW_GAMEOBJECT_NAME).AddComponent<UnityServiceView>();
        }

        public void OnChangeGameFocus(bool focus)
        {
            _eventBusService.Send(new EventOnUnityFocusChanged(focus));
        }

        public void OnChangeGamePause(bool pause)
        {
            _clockService.SetPause(pause);
            _eventBusService.Send(new EventOnUnityPausedChanged(pause));
        }
    }
}