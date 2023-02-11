using Urd.Ads;

namespace Urd.Services.Ads
{
    public interface IAdsServiceProvider
    {
        void Init();
        void ShowBanner(AdsBannerModel adsBannerModel);
        void HideBanner();
        void ShowInterstitial();
        void HideInterstitial();
        void ShowRewardedVideo();
        void HideRewardedVideo();
    }
}