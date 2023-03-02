using Urd.Ads;
using Urd.Services;
using Urd.Utils;

namespace Urd.DebugTools
{
    public class DebugAdsInterstitialShow : DebugAbstract
    {
        public override void OnInputGetDown()
        {
            StaticServiceLocator.Get<IAdsService>().ShowInterstitial();
        }
    }
}