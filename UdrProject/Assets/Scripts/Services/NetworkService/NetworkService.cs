using System;
using System.Collections;
using UnityEngine.Networking;
using Urd.Error;
using Urd.Services.Network;

namespace Urd.Services
{
    public class NetworkService : BaseService, INetworkService
    {
        private ICoroutineService _coroutineService;
        private ICacheService _cacheService;

        public override void Init()
        {
            _coroutineService = ServiceLocatorService.Get<ICoroutineService>();
            _cacheService = ServiceLocatorService.Get<ICacheService>();

            base.Init();
        }

        public void Request(NetworkRequestModel networkRequestModel, Action<NetworkRequestModel> onRequestHttpFinishedSuccess, Action<ErrorModel> onRequestHttpFinishedFailed)
        {
            _coroutineService.StartCoroutine(RequestCo(networkRequestModel, onRequestHttpFinishedSuccess, onRequestHttpFinishedFailed));
        }

        private IEnumerator RequestCo(NetworkRequestModel networkRequestModel, Action<NetworkRequestModel> onRequestHttpFinishedSuccess, Action<ErrorModel> onRequestHttpFinishedFailed)
        {
            var unityWebRequest = GetWebRequest(networkRequestModel);

            yield return unityWebRequest.SendWebRequest();

            if(unityWebRequest.result == UnityWebRequest.Result.Success)
            {
                networkRequestModel.SetResponseData(unityWebRequest.downloadHandler.text);
                onRequestHttpFinishedSuccess?.Invoke(networkRequestModel);
            }
            else
            {
                networkRequestModel.SetErrorResponse(unityWebRequest.error, unityWebRequest.result);
                var error = new ErrorModel(unityWebRequest.error, unityWebRequest.responseCode, unityWebRequest.result);
                onRequestHttpFinishedFailed?.Invoke(error);
            }
        }

        private UnityWebRequest GetWebRequest(NetworkRequestModel networkRequestModel)
        {
            switch (networkRequestModel.RequestType)
            {
                case NetworkRequestType.Get:
                    return UnityWebRequest.Get(networkRequestModel.Url);
                case NetworkRequestType.Post:
                    return UnityWebRequest.Post(networkRequestModel.Url, networkRequestModel.PostData);
                case NetworkRequestType.Put:
                    return UnityWebRequest.Put(networkRequestModel.Url, networkRequestModel.PutData);
                case NetworkRequestType.Head:
                    return UnityWebRequest.Head(networkRequestModel.Url);
                default: return UnityWebRequest.Get(networkRequestModel.Url);
            }
        }
    }
}