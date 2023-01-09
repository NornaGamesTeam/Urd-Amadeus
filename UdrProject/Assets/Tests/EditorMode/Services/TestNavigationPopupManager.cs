using NSubstitute;
using NUnit.Framework;
using System;
using Urd.Services;
using Urd.Services.Navigation;

namespace Urd.Test
{
    public class TestNavigationPopupManager
    {
        private IAssetService _assetService;
        private INavigationService _navigationService;

        private PopupModel _popupModel;
        private bool _onOpenCallback;

        [SetUp]
        public void SetUp()
        {
            IServiceLocator serviceLocator = new ServiceLocator();
            var assetService = Substitute.For<IAssetService>();
            serviceLocator.Register<IAssetService>(assetService);
            
            _navigationService = new NavigationService();
            _navigationService.SetServiceLocatorService(serviceLocator);
            _navigationService.Init();

            _popupModel = new PopupModel(PopupType.Info);
        }

        [Test]
        public void NavigationService_Open_Success()
        {
            _navigationService.Open(_popupModel, OnOpenNavegable);

            Assert.That(_onOpenCallback, Is.True);
        }

        [Test]
        public void NavigationService_IsOpen_Success()
        {
            _navigationService.Open(_popupModel, OnOpenNavegable);

            bool isOpen = _navigationService.IsOpen(_popupModel);
            
            Assert.That(_onOpenCallback && isOpen, Is.True);
        }

        [Test]
        public void NavigationService_Close_Success()
        {
            _navigationService.Open(_popupModel, OnOpenNavegable);
            _navigationService.Close(_popupModel, OnOpenNavegable);

            bool isOpen = _navigationService.IsOpen(_popupModel);

            Assert.That(_onOpenCallback && !isOpen, Is.True);
        }

        private void OnOpenNavegable(bool success)
        {
            _onOpenCallback = success;
        }

        private class NavigableArbitraryClass : Navigable
        {
            public override string Id => "NavigableArbitraryClass";
        }
    }
}