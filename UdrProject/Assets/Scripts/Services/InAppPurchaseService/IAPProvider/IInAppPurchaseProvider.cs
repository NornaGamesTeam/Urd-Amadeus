using System;
using System.Collections.Generic;

namespace Urd.Services.InAppPurchase
{
    public interface IInAppPurchaseProvider
    {
        void LoadProducts(Action<List<InAppPurchaseStoreProducts>> onProductsProvided);
        void Init(Action onInitializedCallback);
    }
}