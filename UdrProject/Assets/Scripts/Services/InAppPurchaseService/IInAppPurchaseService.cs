
using System;

namespace Urd.Services
{
    public interface IInAppPurchaseService : IBaseService
    {
        public void Purchase(string productId, Action<bool> onPurchaseCallback);
        public void RestoreTransactions(Action<bool> onRestoreTransaction);
    }
}