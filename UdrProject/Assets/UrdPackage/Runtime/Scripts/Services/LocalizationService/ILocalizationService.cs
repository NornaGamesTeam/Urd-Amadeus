using System.Globalization;

namespace Urd.Services
{
    public interface ILocalizationService : IBaseService
    {
        CultureInfo Language { get; }
        string Locate(string key);
        bool TryLocate(string key, out string value);
    }
}