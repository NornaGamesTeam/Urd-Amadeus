using Urd.Ads;
using Urd.Services.Ads;

namespace Urd.Services
{
    public interface IAdsService : IBaseService
    {
        void SetProvider(IAdsServiceProvider provider);
        void ShowBanner(AdsBannerModel adsBannerModel);
        void HideBanner();

        void ShowInterstitial();
        void HideInterstitial();

        void ShowRewardedVideo();
        void HideRewardedVideo();
    }
}