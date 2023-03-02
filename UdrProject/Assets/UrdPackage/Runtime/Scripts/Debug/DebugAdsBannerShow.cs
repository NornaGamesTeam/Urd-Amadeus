using Urd.Ads;
using Urd.Services;
using Urd.Utils;

namespace Urd.DebugTools
{
    public class DebugAdsBannerShow : DebugAbstract
    {
        public override void OnInputGetDown()
        {
            var adsBannerModel = new AdsBannerModel();
            StaticServiceLocator.Get<IAdsService>().ShowBanner(adsBannerModel);
        }
    }
}