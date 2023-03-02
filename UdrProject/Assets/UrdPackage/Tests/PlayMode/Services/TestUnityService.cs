using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.TestTools;
using Urd.Services;

namespace Urd.Test
{
    public class TestUnityService
    {
        IUnityService _unityService;
        IClockService _clockService;

        [SetUp]
        public void SetUp()
        {
            IServiceLocator serviceLocator = new ServiceLocator();

            var coroutineService = new CoroutineService();
            coroutineService.SetServiceLocatorService(serviceLocator);
            serviceLocator.Register<ICoroutineService>(coroutineService);

            var eventButService = new EventBusService();
            eventButService.SetServiceLocatorService(serviceLocator);
            serviceLocator.Register<IEventBusService>(eventButService);

            _clockService = new ClockService();
            _clockService.SetServiceLocatorService(serviceLocator);
            serviceLocator.Register<IClockService>(_clockService);

            _unityService = new UnityService();
            _unityService.SetServiceLocatorService(serviceLocator);
            serviceLocator.Register<IUnityService>(_unityService);
        }

        [UnityTest]
        public IEnumerator UnityService_SetGamePause_Success()
        {
            EditorApplication.pauseStateChanged += OnPauseStateChange;
            yield return null;
            
            EditorApplication.isPaused = true;
            //_unityService.OnChangeGamePause(true);
            yield return null;

            // TODO Test for unity Service
            // TODO Test for unity ServiceI
            //Assert.That(_clockService.IsInPause, Is.True);
        }

        private void OnPauseStateChange(PauseState onPause)
        {
            EditorApplication.isPaused = false;
        }
    }
}