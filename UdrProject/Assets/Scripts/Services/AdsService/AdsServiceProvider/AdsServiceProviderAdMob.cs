using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Editor;
using Mono.Cecil;
using UnityEngine;
using Urd.Ads;

namespace Urd.Services.Ads
{
    public class AdsServiceProviderAdMob : AdsServiceProvider
    {
        private const string ADMOB_CONFIG_FILE_PATH = "GoogleMobileAdsSettings"; 
        
        private BannerView _banner;
        private InterstitialAd _interstitialAd;

        public GoogleMobileAdsSettings _adMobSettings;
        public bool IsInitialized { get; private set; }
        public override void Init()
        {
            base.Init();

            LoadConfig();
            MobileAds.Initialize(initialized => IsInitialized = true);
        }

        private void LoadConfig()
        {
            _adMobSettings = Resources.Load<GoogleMobileAdsSettings>(ADMOB_CONFIG_FILE_PATH);
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
            InterstitialAd.Load(GetAdUnitId(), request, OnLoadInterstitial );
        }

        private void OnLoadInterstitial(InterstitialAd newInsterstitialAd, LoadAdError loadAdError)
        {
            _interstitialAd = newInsterstitialAd;
            _interstitialAd.Show();
        }

        public override void HideInterstitial()
        {
            _interstitialAd.Destroy();
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