using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using Urd.Services;
using Urd.Services.Localization;

namespace Urd.Test
{
    public class TestLocalizationService
    {
        private ILocalizationService _localizationService;

        public const string ArbitrarySuccessKey = "ACCEPT_KEY";
        public const string ArbitraryValue = "accept";
        public const string ArbitraryWrongKey = "WRONG_KEY";

        [SetUp]
        public void SetUp()
        {
            IServiceLocator serviceLocator = new ServiceLocator();

            var remoteConfigService = Substitute.For<IRemoteConfigurationService>();
            serviceLocator.Register<IRemoteConfigurationService>(remoteConfigService);

            var eventBusService = Substitute.For<IEventBusService>();
            serviceLocator.Register<IEventBusService>(eventBusService);

            _localizationService = new LocalizationService();
            _localizationService.SetServiceLocatorService(serviceLocator);
            serviceLocator.Register<ILocalizationService>(_localizationService);
        }

        [Test]
        public void UnityService_TryLocate_Success()
        {
            if(_localizationService.TryLocate(ArbitrarySuccessKey, out var stringValue))
            {
                
            }

            Assert.That(stringValue, Is.EqualTo(ArbitraryValue));
        }

        [Test]
        public void UnityService_TryLocate_Failed()
        {
            if (_localizationService.TryLocate(ArbitraryWrongKey, out var stringValue))
            {

            }

            Assert.That(stringValue, Is.EqualTo(ArbitraryWrongKey));
        }
    }
}