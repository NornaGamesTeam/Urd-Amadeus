using System;
using System.Collections.Generic;

namespace Urd.Services.Localization
{
    public class DummyLocalizationKeysProvider : ILocalizationKeysProvider
    {
        public void GetLocalization(LocalizationLanguages language, Action<Dictionary<string, string>> localizationKeyValuesCallback)
        {
            var localizationKeyValues = new Dictionary<string, string>();
            localizationKeyValues.Add("OK", "ok");

            localizationKeyValuesCallback?.Invoke(localizationKeyValues);
        }
    }
}