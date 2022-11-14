using System;
using TMPro;
using UnityEngine;
using Urd.Services;
using Urd.Services.Localization;
using Urd.Utils;

namespace Urd.View.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizationView : MonoBehaviour, IEventBusObservable<EventOnLocalizationChanged>
    {
        public string Key => _key;

        [SerializeField]
        private string _key;
        [SerializeField]
        private string _value;

        private ILocalizationService _localizationService;

        private TextMeshProUGUI _textMeshProUGUI;
        private TextMeshProUGUI TextMeshProUGUI
        {
            get
            {
                if (_textMeshProUGUI == null)
                {
                    _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
                }
                return _textMeshProUGUI;
            }
        }

        private void Start()
        {
            _localizationService = StaticServiceLocator.Get<ILocalizationService>();

            SetKeyValueToTextMeshPro();
        }

        public void OnNewEvent(EventOnLocalizationChanged newEvent)
        {
            SetKeyValueToTextMeshPro();
        }

        public void SetKey(string newKey)
        {
            _key = newKey;
        }

        public void SeValueFromKey()
        {
            _value = GetValueFromKey();
        }

        public void SetKeyValueToTextMeshPro()
        {
            if (string.IsNullOrEmpty(Key))
            {
                return;
            }
            _value = GetValueFromKey();
            SetTextMeshProValue(_value);
        }

        public void SetTextMeshProValue(string textMeshProValue)
        {
            TextMeshProUGUI.text = textMeshProValue;
        }

        private string GetValueFromKey()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                return GetValueFromKeyEditor();
            }
#endif

            return _localizationService.Locate(Key);
        }
#if UNITY_EDITOR
        private string GetValueFromKeyEditor()
        {
            return UrdEditor.EditorLocalizationService.Locate(Key);
        }

#endif
    }
}