using Urd.Ads;

namespace Urd.Services.Ads
{
    public abstract class AdsServiceProvider : IAdsServiceProvider
    {
        public virtual void Init() { }

        public abstract void ShowBanner(AdsBannerModel adsBannerModel);

        public abstract void HideBanner();
    }
}