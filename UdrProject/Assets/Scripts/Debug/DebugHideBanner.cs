using Urd.Ads;
using Urd.Services;
using Urd.Utils;

namespace Urd.DebugTools
{
    public class DebugHideBanner : DebugAbstract
    {
        public override void OnInputGetDown()
        {
            StaticServiceLocator.Get<IAdsService>().HideBanner();
        }
    }
}