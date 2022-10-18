using UnityEngine;

namespace Urd.Services
{
    public class UnityService : BaseService, IUnityService
    {
        private static string UNITY_VIEW_GAMEOBJECT_NAME = "UnityServiceView";

        private UnityServiceView _unityServiceView;

        private IClockService _clockService;

        public override void Init()
        {
            base.Init();

            _clockService = ServiceLocatorService.Get<IClockService>();

            CreateCoroutineView();
        }

        private void CreateCoroutineView()
        {
            _unityServiceView = new GameObject(UNITY_VIEW_GAMEOBJECT_NAME).AddComponent<UnityServiceView>();

            if (Application.isPlaying)
            {
                GameObject.DontDestroyOnLoad(_unityServiceView);
            }
        }

        public void OnChangeGameFocus(bool focus)
        {
            // TODO event bus for game Focus
        }

        public void OnChangeGamePause(bool pause)
        {
            // TODO event bus for game pause
            _clockService.SetPause(pause);
        }
    }
}