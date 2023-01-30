using Urd.Ads;
using Urd.Services.Ads;

namespace Urd.Services
{
    public class AdsService : BaseService, IAdsService
    {
        private IAdsServiceProvider _adsServiceProvider;
        
        public override void Init()
        {
            base.Init();
            
            GetProvider();
            
            _adsServiceProvider.Init();
        }

        private void GetProvider()
        {
            _adsServiceProvider = new AdsServiceProviderAdMob();
        }

        public void ShowBanner(AdsBannerModel adsBannerModel) => _adsServiceProvider.ShowBanner(adsBannerModel);
        public void HideBanner() => _adsServiceProvider.HideBanner();
        public void ShowInterstitial() => _adsServiceProvider.ShowInterstitial();
        public void HideInterstitial() => _adsServiceProvider.HideInterstitial();
        public void ShowRewardedVideo() => _adsServiceProvider.ShowRewardedVideo();
        public void HideRewardedVideo() => _adsServiceProvider.HideRewardedVideo();
    }
}