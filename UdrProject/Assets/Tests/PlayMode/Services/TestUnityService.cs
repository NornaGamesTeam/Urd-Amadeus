using System.Collections;
using NUnit.Framework;
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
            _unityService = new UnityService();

            IServiceLocator serviceLocator = new ServiceLocator();
            serviceLocator.Register<ICoroutineService>(new CoroutineService());
            _clockService = new ClockService();
            _clockService.SetServiceLocatorService(serviceLocator);
            serviceLocator.Register<IClockService>(_clockService);

            _unityService.SetServiceLocatorService(serviceLocator);

            _clockService.Init();
            _unityService.Init();
        }

        [UnityTest]
        public IEnumerator UnityService_SetGamePause_Success()
        {
            yield return null;
            _unityService.OnChangeGamePause(true);

            Assert.That(_clockService.IsInPause, Is.True);
        }
    }
}