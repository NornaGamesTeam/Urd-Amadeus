using System;
using UnityEngine.Purchasing;

namespace Urd.Services.InAppPurchase
{
    public interface IInAppPurchaseStoreListener : IStoreListener
    {
        event Action<IStoreController, IExtensionProvider> OnInitializedEvent;
        event Action<InitializationFailureReason> OnInitializeFailedEvent;
        event Func<PurchaseEventArgs, PurchaseProcessingResult> ProcessPurchaseEvent;
        event Action<Product, PurchaseFailureReason> OnPurchaseFailedEvent;
    }
}