using System;
using System.Collections.Generic;
using Urd.Services.Localization;

namespace Urd.Editor
{
    public interface IEditorLocalizationServiceProvider
    {
        void FetchLocalization(LocalizationLanguages language, Action<Dictionary<string, string>> onLocalizationFetched);
    }
}