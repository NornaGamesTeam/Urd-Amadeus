using System;
using UnityEngine.Purchasing;

namespace Urd.Services.InAppPurchase
{
    public class InAppPurchaseStoreListener : IInAppPurchaseStoreListener
    {
        public event Action<IStoreController, IExtensionProvider> OnInitializedEvent;
        public event Action<InitializationFailureReason> OnInitializeFailedEvent;
        public event Func<PurchaseEventArgs, PurchaseProcessingResult> ProcessPurchaseEvent;
        public event Action<Product, PurchaseFailureReason> OnPurchaseFailedEvent;
        
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            OnInitializedEvent?.Invoke(controller,extensions);
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            OnInitializeFailedEvent?.Invoke(error);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            OnInitializeFailedEvent?.Invoke(error);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            if (ProcessPurchaseEvent != null)
            {
                return ProcessPurchaseEvent.Invoke(purchaseEvent);
            }

            return PurchaseProcessingResult.Pending;
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            OnPurchaseFailedEvent?.Invoke(product, failureReason);
        }

    }
}