using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Editor;
using UnityEngine;
using Urd.Ads;

namespace Urd.Services.Ads
{
    [Serializable]
    public class AdsServiceProviderAdMob : AdsServiceProvider
    {
        private const string ADMOB_CONFIG_FILE_PATH = "GoogleMobileAdsSettings"; 
        
        private BannerView _banner;
        private InterstitialAd _interstitialAd;
        private RewardedAd _rewardedVideo;

        [SerializeField]
        private GoogleMobileAdsSettings _adMobSettings;
        public bool IsInitialized { get; private set; }
        public override void Init(Action onInitializeCallback)
        {
            base.Init(onInitializeCallback);
            
            MobileAds.Initialize(
                status => OnInitialize(status, onInitializeCallback));
        }

        private void OnInitialize(InitializationStatus status, Action onInitializeCallback)
        {
            IsInitialized = true;
            onInitializeCallback?.Invoke();
        }

        public override void ShowBanner(AdsBannerModel adsBannerModel)
        {
            if (_banner?.IsDestroyed == false)
            {
                HideBanner();
            }
            
            _banner = new BannerView(GetAdUnitId(),
                                     new AdSize(adsBannerModel.Size.x, adsBannerModel.Size.y),
                                     GetAdsPosition(adsBannerModel));


            var request = new AdRequest.Builder().Build();
            _banner.LoadAd(request);
        }
        
        public override void HideBanner()
        {
            _banner?.Destroy();
        }

        public override void ShowInterstitial()
        {
            var request = new AdRequest.Builder().Build();
            InterstitialAd.Load(GetAdUnitId(), request, OnInterstitialLoaded );
        }

        private void OnInterstitialLoaded(InterstitialAd interstitial, LoadAdError loadAdError)
        {
            _interstitialAd = interstitial;
            if (_interstitialAd.CanShowAd())
            {
                _interstitialAd.Show();
            }
        }

        public override void HideInterstitial()
        {
            _interstitialAd.Destroy();
        }

        public override void ShowRewardedVideo()
        {
            var request = new AdRequest.Builder().Build();
            RewardedAd.Load(GetAdUnitId(), request, OnRewardedVideoLoaded); 
        }

        private void OnRewardedVideoLoaded(RewardedAd rewardedVideo, LoadAdError loadAdError)
        {
            _rewardedVideo = rewardedVideo;
            if (_rewardedVideo.CanShowAd())
            {
                var reward = _rewardedVideo.GetRewardItem();
                reward.Type = "temp";
                reward.Amount = 11;
                _rewardedVideo.Show(OnRewardedVideoShowed);
            }
        }

        private void OnRewardedVideoShowed(Reward reward)
        {
        }

        public override void HideRewardedVideo()
        {
            throw new System.NotImplementedException();
        }

        private string GetAdUnitId()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android: return _adMobSettings.GoogleMobileAdsAndroidAppId;                 
                case RuntimePlatform.IPhonePlayer: return _adMobSettings.GoogleMobileAdsIOSAppId;
                default: return _adMobSettings.GoogleMobileAdsAndroidAppId;
            }
        }
        
        private AdPosition GetAdsPosition(AdsBannerModel adsBannerModel)
        {
            switch (adsBannerModel.Position)
            {
                case AdsBannerPosition.Top: return AdPosition.Top;
                case AdsBannerPosition.Bottom:return AdPosition.Bottom;
                case AdsBannerPosition.TopLeft:return AdPosition.TopLeft;
                case AdsBannerPosition.TopRight:return AdPosition.TopRight;
                case AdsBannerPosition.BottomLeft:return AdPosition.BottomLeft;
                case AdsBannerPosition.BottomRight:return AdPosition.BottomRight;
                case AdsBannerPosition.Center:return AdPosition.Center;
                default: return AdPosition.Top;
            }
        }
    }
}