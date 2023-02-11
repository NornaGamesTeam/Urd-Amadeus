using System.Collections.Generic;
using NSubstitute;
using UnityEngine;
using UnityEngine.Purchasing;
using Urd.Error;
using Urd.Services.InAppPurchase;
using Urd.Services.RemoteConfiguration;
using Urd.Utils;

namespace Urd.Services
{
    public class InAppPurchaseService : BaseService, IInAppPurchaseService
    {
        private IInAppPurchaseProvider _provider;

        private IInAppPurchaseStoreListener _storeListener;
        
        private ConfigurationBuilder _storeBuilder;
        private IStoreController _storeController;
        public override void Init()
        {
            base.Init();

            _provider = GetProvider();
            LoadProducts();
        }

        private IInAppPurchaseProvider GetProvider()
        {
            return new InAppPurchaseProviderRemoteConfig();
        }

        private void LoadProducts()
        {
            _provider.LoadProducts(OnProductsProvided);
        }

        private void OnProductsProvided(List<InAppPurchaseStoreProducts> newProducts)
        {
            _storeBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            for (int i = 0; i < newProducts.Count; i++)
            {
                var product = newProducts[i];
                _storeBuilder.AddProduct(product.Id, product.Type, product.StoreIds);
            }

            InitStore();
        }

        private void InitStore()
        {
            _storeListener = GetListener();
            
            UnityPurchasing.Initialize(_storeListener, _storeBuilder);
        }

        private IInAppPurchaseStoreListener GetListener()
        {
            var listener = new InAppPurchaseStoreListener();
            listener.OnInitializedEvent += OnInitialized;
            listener.OnInitializeFailedEvent += OnInitializeFailed;
            listener.ProcessPurchaseEvent += ProcessPurchase;
            listener.OnPurchaseFailedEvent += OnPurchaseFailed;
            return listener;
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
        }
        
        public void OnInitializeFailed(InitializationFailureReason error)
        {
            var errorModel = new ErrorModel($"[InAppPurchaseService] Error when try to initialize the store: {error}",
                                       ErrorCode.Error_999_Unknown_Error);
            Debug.LogWarning(errorModel.ToString());
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            throw new System.NotImplementedException();
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            throw new System.NotImplementedException();
        }

    }
}
