using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using Urd.Services.Localization;

namespace Urd.Services
{
    [Serializable]
    public class LocalizationService : BaseService, ILocalizationService
    {
        private const string MAIN_TABLE_REFERENCE = "MainTable";
            
        public CultureInfo Language { get; }

        private IEventBusService _eventBusService;
        
        public override void Init()
        {
            base.Init();

            LoadLanguage();
            _eventBusService = ServiceLocatorService.Get<IEventBusService>();
        }
        private void LoadLanguage()
        {
            var initializationOperation = LocalizationSettings.InitializationOperation;
            initializationOperation.Completed += OnInitializeLocalization;
        }

        private void OnInitializeLocalization(AsyncOperationHandle<LocalizationSettings> task)
        {
            SetAsLoaded();
            
            _eventBusService.Send(new EventOnLocalizationChanged());
        }

        public string Locate(string key)
        {
            TryLocate(key, out var value);
            return value;
        }

        public bool TryLocate(string key, out string value)
        {
            var stringReference = new LocalizedString()
            {
                TableReference = MAIN_TABLE_REFERENCE,
                TableEntryReference = key,
            };
            value = stringReference.GetLocalizedString();
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}