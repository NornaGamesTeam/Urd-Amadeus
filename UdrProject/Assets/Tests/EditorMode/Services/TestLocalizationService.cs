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
        private ILocalizationKeysProvider _testDummyLocalizationProvider;

        public const string ArbitrarySuccessKey = "SUCCESS_KEY";
        public const string ArbitraryValue = "Success";
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

            _testDummyLocalizationProvider = new TestDummyLocalizationProvider();
            _localizationService.SetProvider(_testDummyLocalizationProvider);
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

        protected class TestDummyLocalizationProvider : ILocalizationKeysProvider
        {
            public void GetLocalization(LocalizationLanguages language, Action<Dictionary<string, string>> localizationKeyValuesCallback)
            {
                var arbitraryProviderKeyValues = new Dictionary<string, string>();
                arbitraryProviderKeyValues.Add(ArbitrarySuccessKey, ArbitraryValue);

                localizationKeyValuesCallback?.Invoke(arbitraryProviderKeyValues);
            }
        }
    }
}