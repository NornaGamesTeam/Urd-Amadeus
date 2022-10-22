using Urd.Services.Localization;

namespace Urd.Services
{
    public interface ILocalizationService : IBaseService
    {
        LocalizationLanguages Language { get; }
        string Locate(string key);
        bool TryLocate(string key, out string value);
    }
}