using System;
using System.Collections;
using System.Drawing.Drawing2D;
using UnityEngine.Assertions.Must;
using UnityEngine.Networking;
using Urd.Services.Network;

namespace Urd.Services
{
    public class NetworkService : BaseService, INetworkService
    {
        private ICoroutineService _coroutineService;

        public override void Init()
        {
            _coroutineService = ServiceLocatorService.Get<ICoroutineService>();

            base.Init();
        }

        public void Request(NetworkRequestModel networkRequestModel, Action<NetworkRequestModel> onRequestHttpFinished)
        {
            _coroutineService.StartCoroutine(RequestCo(networkRequestModel, onRequestHttpFinished));
        }

        private IEnumerator RequestCo(NetworkRequestModel networkRequestModel, Action<NetworkRequestModel> onRequestHttpFinished)
        {
            var unityWebRequest = GetWebRequest(networkRequestModel);

            yield return unityWebRequest.SendWebRequest();

            if(unityWebRequest.result == UnityWebRequest.Result.Success)
            {
                networkRequestModel.SetResponseData(unityWebRequest.downloadHandler.text);
                onRequestHttpFinished?.Invoke(networkRequestModel);
            }
            else
            {
                networkRequestModel.SetErrorResponse(unityWebRequest.error, unityWebRequest.result);
                onRequestHttpFinished?.Invoke(networkRequestModel);
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