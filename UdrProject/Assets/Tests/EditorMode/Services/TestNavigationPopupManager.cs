using NSubstitute;
using NUnit.Framework;
using System;
using Urd.Popup;
using Urd.Services;
using Urd.Services.Navigation;

namespace Urd.Test
{
    public class TestNavigationPopupManager
    {
        private IAssetService _assetService;
        private INavigationService _navigationService;

        private PopupInfoModel _popupInfoModel;
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

            _popupInfoModel = new PopupInfoModel();
        }

        [Test]
        public void NavigationService_Open_Success()
        {
            _navigationService.Open(_popupInfoModel, OnOpenNavigable);

            Assert.That(_onOpenCallback, Is.True);
        }

        [Test]
        public void NavigationService_IsOpen_Success()
        {
            _navigationService.Open(_popupInfoModel, OnOpenNavigable);

            bool isOpen = _navigationService.IsOpen(_popupInfoModel);
            
            Assert.That(_onOpenCallback && isOpen, Is.True);
        }

        [Test]
        public void NavigationService_Close_Success()
        {
            _navigationService.Open(_popupInfoModel, OnOpenNavigable);
            _navigationService.Close(_popupInfoModel, OnOpenNavigable);

            bool isOpen = _navigationService.IsOpen(_popupInfoModel);

            Assert.That(_onOpenCallback && !isOpen, Is.True);
        }

        private void OnOpenNavigable(bool success)
        {
            _onOpenCallback = success;
        }
    }
}