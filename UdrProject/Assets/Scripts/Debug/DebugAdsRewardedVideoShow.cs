using Urd.Ads;
using Urd.Services;
using Urd.Utils;

namespace Urd.DebugTools
{
    public class DebugAdsRewardedVideoShow : DebugAbstract
    {
        public override void OnInputGetDown()
        {
            StaticServiceLocator.Get<IAdsService>().ShowRewardedVideo();
        }
    }
}