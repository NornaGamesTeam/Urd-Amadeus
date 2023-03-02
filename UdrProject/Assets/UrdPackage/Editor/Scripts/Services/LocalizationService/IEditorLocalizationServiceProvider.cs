using System;
using System.Collections.Generic;
using Urd.Services.Localization;

namespace Urd.UrdEditor
{
    public interface IEditorLocalizationServiceProvider
    {
        void FetchLocalization(LocalizationLanguages language, Action<Dictionary<string, string>> onLocalizationFetched);
    }
}