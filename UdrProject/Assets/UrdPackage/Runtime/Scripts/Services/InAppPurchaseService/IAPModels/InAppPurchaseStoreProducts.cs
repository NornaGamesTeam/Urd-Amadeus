using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine.Purchasing;

namespace Urd.Services.InAppPurchase
{
    public class InAppPurchaseStoreProducts
    {
        [JsonProperty]
        public string Id { get; private set; }
        [JsonProperty]
        public ProductType Type { get; private set; }
        [JsonProperty] 
        private KeyValuePair<string, string>[] _storeIds;

        public IDs StoreIds
        {
            get
            {
                if (_storeIds == null || _storeIds.Length <= 0)
                {
                    return null;
                }

                var storeIds = new IDs();

                for (int i = 0; i < _storeIds.Length; i++)
                {
                    StoreIds.Add(_storeIds[i].Key, _storeIds[i].Value);
                }

                return storeIds;
            }
        }

        public InAppPurchaseStoreProducts(string id): this(
            id,
            ProductType.Consumable,
            new KeyValuePair<string,string>(id, GooglePlay.Name),
            new KeyValuePair<string,string>(id, AppleAppStore.Name)
            ) {
        }

        private InAppPurchaseStoreProducts(string id, ProductType type, params KeyValuePair<string, string>[] storeIds)
        {
            Id = id;
            Type = type;

            _storeIds = storeIds;
        }
    }
}