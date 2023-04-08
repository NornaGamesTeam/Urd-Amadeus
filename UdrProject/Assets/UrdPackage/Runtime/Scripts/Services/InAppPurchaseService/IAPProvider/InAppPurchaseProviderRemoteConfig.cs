using System;
using System.Collections.Generic;
using UnityEngine;
using Urd.Services.InAppPurchase;
using Urd.Utils;

namespace Urd.Services.RemoteConfiguration
{
    [Serializable]
    public class InAppPurchaseProviderRemoteConfig : IInAppPurchaseProvider
    {
        private const string STORE_KEY = "Store";

        private ServiceHelper<IRemoteConfigurationService> _remoteConfigService =
            new ServiceHelper<IRemoteConfigurationService>();
        public InAppPurchaseProviderRemoteConfig()
        {
            
        }
        
        public void Init(Action onInitializedCallback)
        {
            onInitializedCallback?.Invoke();
        }
        
        public void LoadProducts(Action<List<InAppPurchaseStoreProducts>> onProductsProvided)
        {
            if (!_remoteConfigService.Service.TryGetDataAs(STORE_KEY, out List<InAppPurchaseStoreProducts> inAppPurchaseStoreProducts))
            {
                Debug.LogWarning($"[InAppPurchaseProviderRemoteConfig] No data for the store in remote config with key: {STORE_KEY}");
                onProductsProvided?.Invoke(new List<InAppPurchaseStoreProducts>());
                return;
            }
            
            onProductsProvided?.Invoke(inAppPurchaseStoreProducts);
        }
    }
}