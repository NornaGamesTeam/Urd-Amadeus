using System;
using UnityEngine.Purchasing;
using Urd.Utils;

namespace Urd.Services.InAppPurchase
{
    public class InAppPurchaseNoReceiptCommunication : IInAppPurchaseCommunication
    {
        private const float FAKE_DELAY_CALL = 0.1f;
        
        private IClockService _clockService;
        private IInAppPurchaseCommunication _inAppPurchaseCommunicationImplementation;

        public InAppPurchaseNoReceiptCommunication()
        {
            _clockService = StaticServiceLocator.Get<IClockService>();
        }
        
        public void SendReceipt(PurchaseEventArgs purchaseEventArgs, Action<Product> onReceiptCompleted)
        {
            _clockService.AddDelayCall(FAKE_DELAY_CALL, () => OnReceiptCompleted(purchaseEventArgs.purchasedProduct, onReceiptCompleted));
        }

        private void OnReceiptCompleted(Product purchasedProduct, Action<Product> onReceiptCompleted)
        {
            onReceiptCompleted?.Invoke(purchasedProduct);
        }
    }
}