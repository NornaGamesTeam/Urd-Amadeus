using NSubstitute;
using NUnit.Framework;
using System;
using Urd.Services;
using Urd.Services.Navigation;

namespace Urd.Test
{
    public class TestNavigationService
    {
        private IAssetService _assetService;
        private INavigationService _navigationService;

        private NavigableArbitraryClass _navigableArbitraryClass;
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

            _navigableArbitraryClass = new NavigableArbitraryClass();
        }

        [Test]
        public void NavigationService_Open_Failed()
        {
            _navigationService.Open(_navigableArbitraryClass, OnOpenNavigable);

            Assert.That(_onOpenCallback, Is.False);
        }

        [Test]
        public void NavigationService_IsOpen_Failed()
        {
            _navigationService.Open(_navigableArbitraryClass, OnOpenNavigable);

            bool isOpen = _navigationService.IsOpen(_navigableArbitraryClass);
            
            Assert.That(_onOpenCallback && isOpen, Is.False);
        }

        [Test]
        public void NavigationService_Close_Failed()
        {
            _navigationService.Open(_navigableArbitraryClass, OnOpenNavigable);
            _navigationService.Close(_navigableArbitraryClass, OnOpenNavigable);

            bool isOpen = _navigationService.IsOpen(_navigableArbitraryClass);

            Assert.That(_onOpenCallback && !isOpen, Is.False);
        }

        private void OnOpenNavigable(bool success)
        {
            _onOpenCallback = success;
        }

        private class NavigableArbitraryClass : Navigable
        {
            public override string Id => "NavigableArbitraryClass";
        }
    }
}