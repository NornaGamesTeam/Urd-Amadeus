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
        [field: SerializeField]
        public string Key { get; private set; }

        private string _value;

        private ILocalizationService _localizationService;
        private TextMeshProUGUI _textMeshProUGUI;

        private void Start()
        {
            _localizationService = StaticServiceLocator.Get<ILocalizationService>();
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();

            SetKeyValueToTextMeshPro();
        }

        public void OnNewEvent(EventOnLocalizationChanged newEvent)
        {
            SetKeyValueToTextMeshPro();
        }

        public void SetKey(string newKey)
        {
            Key = newKey;
        }

        [ContextMenu("SetKeyValueToTextMeshPro")]
        public void SetKeyValueToTextMeshPro()
        {
            if (string.IsNullOrEmpty(Key))
            {
                return;
            }

            _textMeshProUGUI.text = GetValueFromKey();
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
            return Editor.EditorLocalizationService.Locate(Key);
        }

#endif
    }
}