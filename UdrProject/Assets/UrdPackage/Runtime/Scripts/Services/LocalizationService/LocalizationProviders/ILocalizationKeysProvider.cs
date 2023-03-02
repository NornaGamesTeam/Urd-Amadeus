using System;
using System.Collections.Generic;

namespace Urd.Services.Localization
{
    public interface ILocalizationKeysProvider
    {
        void GetLocalization(LocalizationLanguages language, Action<Dictionary<string, string>> localizationKeyValuesCallback);
    }
}