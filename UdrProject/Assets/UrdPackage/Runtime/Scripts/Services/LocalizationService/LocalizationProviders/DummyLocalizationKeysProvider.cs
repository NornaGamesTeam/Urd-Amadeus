using System;
using System.Collections.Generic;

namespace Urd.Services.Localization
{
    [Serializable]
    public class DummyLocalizationKeysProvider : ILocalizationKeysProvider
    {
        public void GetLocalization(LocalizationLanguages language, Action<Dictionary<string, string>> localizationKeyValuesCallback)
        {
            localizationKeyValuesCallback?.Invoke(new Dictionary<string, string>());
        }
    }
}