using System.IO;
using UnityEngine;

namespace GoogleMobileAds.Editor
{
    public class GoogleMobileAdsSettings : ScriptableObject
    {
        public const string MobileAdsSettingsResDir = "Assets/GoogleMobileAds/Resources";

        public const string MobileAdsSettingsFile = "GoogleMobileAdsSettings";

        public const string MobileAdsSettingsFileExtension = ".asset";

        public static GoogleMobileAdsSettings LoadInstance()
        {
            //Read from resources.
            var instance = Resources.Load<GoogleMobileAdsSettings>(MobileAdsSettingsFile);

            //Create instance if null.
            if (instance == null)
            {
                Directory.CreateDirectory(MobileAdsSettingsResDir);
                instance = ScriptableObject.CreateInstance<GoogleMobileAdsSettings>();
            }

            return instance;
        }

        [SerializeField]
        private string adMobAndroidAppId = string.Empty;

        [SerializeField]
        private string adMobIOSAppId = string.Empty;

        [SerializeField]
        private bool delayAppMeasurementInit;

        [SerializeField]
        private bool optimizeInitialization;

        [SerializeField]
        private bool optimizeAdLoading;

        [SerializeField]
        private string userTrackingUsageDescription;

        public string GoogleMobileAdsAndroidAppId
        {
            get { return adMobAndroidAppId; }

            set { adMobAndroidAppId = value; }
        }

        public string GoogleMobileAdsIOSAppId
        {
            get { return adMobIOSAppId; }

            set { adMobIOSAppId = value; }
        }

        public bool DelayAppMeasurementInit
        {
            get { return delayAppMeasurementInit; }

            set { delayAppMeasurementInit = value; }
        }

        public bool OptimizeInitialization
        {
            get { return optimizeInitialization; }

            set { optimizeInitialization = value; }
        }

        public bool OptimizeAdLoading
        {
            get { return optimizeAdLoading; }

            set { optimizeAdLoading = value; }
        }

        public string UserTrackingUsageDescription
        {
            get { return userTrackingUsageDescription; }

            set { userTrackingUsageDescription = value; }
        }
    }
}
