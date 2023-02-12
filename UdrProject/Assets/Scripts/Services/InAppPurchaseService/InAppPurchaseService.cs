using System;
using System.Collections.Generic;
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
        private IInAppPurchaseCommunication _storeCommunication;
        
        private ConfigurationBuilder _storeBuilder;
        private IStoreController _storeController;
        private IExtensionProvider _storeExtensions;
        
        private Action<bool> _onPurchaseCallback;

        public override void Init()
        {
            base.Init();

            _provider = GetProvider();
            _storeCommunication = GetCommunication();
            LoadProducts();
        }
        
        private IInAppPurchaseProvider GetProvider()
        {
            return new InAppPurchaseProviderRemoteConfig();
        }
        
        private IInAppPurchaseCommunication GetCommunication()
        {
            return new InAppPurchaseNoReceiptCommunication();
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

        private void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
            _storeExtensions = extensions;
        }
        
        private void OnInitializeFailed(InitializationFailureReason error)
        {
            var errorModel = new ErrorModel($"[InAppPurchaseService] Error when try to initialize the store: {error}",
                                       ErrorCode.Error_999_Unknown_Error);
            Debug.LogWarning(errorModel.ToString());
        }

        public void Purchase(string productId, Action<bool> onPurchaseCallback)
        {
            _onPurchaseCallback = onPurchaseCallback;
            
            _storeController.InitiatePurchase(productId);
        }
        
        private PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            CallPurchaseCallback(true);

            _storeCommunication.SendReceipt(purchaseEvent, OnReceiptCompleted);
            return PurchaseProcessingResult.Pending;
        }
        
        private void OnReceiptCompleted(Product product)
        {
            if (product == null)
            {
                CallPurchaseCallback(false);
                return;
            }
            
            _storeController.ConfirmPendingPurchase(product);
            CallPurchaseCallback(true);
        }

        private void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            ErrorModel errorModel = new ErrorModel($"[InAppPruchaseService] Error when try to purchase the " +
                                                   $"product with id: {product}: {failureReason}",
                                                   ErrorCode.Error_999_Unknown_Error);
            Debug.LogWarning(errorModel.ToString());

            CallPurchaseCallback(false);
        }

        private void CallPurchaseCallback(bool success)
        {
            _onPurchaseCallback?.Invoke(success);
            _onPurchaseCallback = null;
        }
        
        public void RestoreTransactions(Action<bool> onRestoreTransaction)
        {
            var appleExtensions = _storeExtensions.GetExtension<IAppleExtensions>();
            if (appleExtensions == null)
            {
                return;
            }
            
            appleExtensions.RestoreTransactions(onRestoreTransaction);
        }
    }
}
