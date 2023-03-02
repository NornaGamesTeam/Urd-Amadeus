using Urd.Ads;

namespace Urd.Services
{
    public interface IAdsService : IBaseService
    {
        void ShowBanner(AdsBannerModel adsBannerModel);
        void HideBanner();

        void ShowInterstitial();
        void HideInterstitial();

        void ShowRewardedVideo();
        void HideRewardedVideo();
    }
}