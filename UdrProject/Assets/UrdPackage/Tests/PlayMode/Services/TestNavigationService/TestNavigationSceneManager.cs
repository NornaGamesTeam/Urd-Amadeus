using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Urd.Scene;
using Urd.Services;

namespace Urd.Test
{
    public class TestNavigationSceneManager
    {
        /// Move to test on play, missing create canvas if not exit at begining
        
        private IAssetService _assetService;
        private INavigationService _navigationService;

        private SceneModel _sceneInfoModel;
        private bool _onOpenCallback;
        private bool _onOpenCallbackValue;
        private bool _onCloseCallback;
        private bool _onCloseCallbackValue;

        [SetUp]
        public void SetUp()
        {
            IServiceLocator serviceLocator = new ServiceLocator();
            
            var coroutineService = new CoroutineService();
            serviceLocator.Register<ICoroutineService>(coroutineService);
            var clockService = new ClockService();
            serviceLocator.Register<IClockService>(clockService);
            
            var assetService = new AssetService();
            serviceLocator.Register<IAssetService>(assetService);
            
            _navigationService = new NavigationService();
            serviceLocator.Register<INavigationService>(_navigationService);

            _sceneInfoModel = new SceneModel(SceneTypes.Test);
        }

        [UnityTest]
        public IEnumerator NavigationSceneManager_Open_Success()
        {
            _navigationService.Open(_sceneInfoModel, OnOpenNavigable);

            yield return new WaitUntil(() => _onOpenCallback);

            Assert.That(_onOpenCallbackValue, Is.True);
        }

        [UnityTest]
        public IEnumerator NavigationSceneManager_IsOpen_Success()
        {
            _navigationService.Open(_sceneInfoModel, OnOpenNavigable);

            yield return new WaitUntil(() => _onOpenCallback);
            bool isOpen = _navigationService.IsOpen(_sceneInfoModel);
            
            Assert.That(_onOpenCallbackValue && isOpen, Is.True);
        }

        [UnityTest]
        public IEnumerator NavigationSceneManager_Close_Success()
        {
            _navigationService.Open(_sceneInfoModel, OnOpenNavigable);
            yield return new WaitUntil(() => _onOpenCallback);
            
            _navigationService.Close(_sceneInfoModel, OnCloseNavigable);
            yield return new WaitUntil(() => _onCloseCallback);

            bool isOpen = _navigationService.IsOpen(_sceneInfoModel);
            Assert.That(_onCloseCallbackValue && !isOpen, Is.True);
        }

        private void OnOpenNavigable(bool success)
        {
            _onOpenCallback = true;
            _onOpenCallbackValue = success;
        }
        
        private void OnCloseNavigable(bool success)
        {
            _onCloseCallback = true;
            _onCloseCallbackValue = success;
        }
    }
}