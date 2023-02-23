using System.Collections.Generic;
using UnityEngine;
using Urd.Services.Localization;

namespace Urd.Services
{
    public class LocalizationService : BaseService, ILocalizationService
    {
        public LocalizationLanguages Language { get; private set; }

        private IEventBusService _eventBusService;

        private Dictionary<string, string> _localizationKeys = new Dictionary<string, string>();
        private ILocalizationKeysProvider _localizationKeysProvider = new DummyLocalizationKeysProvider();

        public override void Init()
        {
            base.Init();

            _eventBusService = ServiceLocatorService.Get<IEventBusService>();

        //private ILocalizationKeysProvider _localizationKeysProvider = new DummyLocalizationKeysProvider();
            LoadLanguage();
            SetDefaultProvider();
        }
        private void LoadLanguage()
        {
            // TODO load last language and if not, read the device one
            Language = LocalizationLanguages.English;
        }

        private void SetDefaultProvider()
        {
            SetProvider(new RemoteConfigurationLocalizationKeyProvider());
        }

        public void SetProvider(ILocalizationKeysProvider localizationKeysProvider)
        {
            _localizationKeysProvider = localizationKeysProvider;
            _localizationKeysProvider.GetLocalization(Language, OnLoadLocalization);
        }

        private void OnLoadLocalization(Dictionary<string, string> localizationKeys)
        {
            SetLocalizationKeysValues(localizationKeys);
            SetAsLoaded();
        }

        private void SetLocalizationKeysValues(Dictionary<string, string> localizationKeys)
        {
            _localizationKeys = localizationKeys;

            _eventBusService.Send(new EventOnLocalizationChanged());
        }

        public string Locate(string key)
        {
            TryLocate(key, out var value);
            return value;
        }

        public bool TryLocate(string key, out string value)
        {
            if(!_localizationKeys.TryGetValue(key, out value))
            {
                value = key;
                Debug.LogWarning($"[LocalizationService] The key {key} is not in the dictionary");
                return false;
            }

            
            return true;
        }
    }
}