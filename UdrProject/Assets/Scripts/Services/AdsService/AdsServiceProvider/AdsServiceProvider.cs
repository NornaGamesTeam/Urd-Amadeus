using Urd.Ads;

namespace Urd.Services.Ads
{
    public abstract class AdsServiceProvider : IAdsServiceProvider
    {
        public virtual void Init() { }

        public abstract void ShowBanner(AdsBannerModel adsBannerModel);

        public abstract void HideBanner();
        public abstract void ShowInterstitial();
        public abstract void HideInterstitial();
    }
}