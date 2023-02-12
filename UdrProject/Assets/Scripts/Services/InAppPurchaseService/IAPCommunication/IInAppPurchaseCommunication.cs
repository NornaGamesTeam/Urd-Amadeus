using System;
using UnityEngine.Purchasing;
using Urd.Utils;

namespace Urd.Services.InAppPurchase
{
    public interface IInAppPurchaseCommunication
    {
        void SendReceipt(PurchaseEventArgs purchaseEventArgs, Action<Product> onReceiptCompleted);
    }
}