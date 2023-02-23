using System;
using Urd.Ads;

namespace Urd.Services.Ads
{
    public abstract class AdsServiceProvider : IAdsServiceProvider
    {
        public virtual void Init(Action onInitializeCallback) { }

        public abstract void ShowBanner(AdsBannerModel adsBannerModel);

        public abstract void HideBanner();
        public abstract void ShowInterstitial();
        public abstract void HideInterstitial();
        public abstract void ShowRewardedVideo();
        public abstract void HideRewardedVideo();
    }
}