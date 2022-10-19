using NUnit.Framework;
using System;
using System.Collections;
using System.Drawing.Text;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.TestTools;
using Urd.Services;
using Urd.Services.Network;

namespace Urd.Test
{
    public class TestNetworkService
    {
        private INetworkService _networkService;

        private NetworkRequestModel _networkRequestModel;
        private UnityWebRequest.Result _requestStatus;

        [SetUp]
        public void SetUp()
        {
            ServiceLocator serviceLocator = new ServiceLocator();

            var coroutineService = new CoroutineService();
            serviceLocator.Register<ICoroutineService>(coroutineService);

            _networkService = new NetworkService();
            serviceLocator.Register<INetworkService>(_networkService);

            var url = "www.google.es";
            _networkRequestModel = new NetworkRequestModel(url);
        }

        [UnityTest]
        public IEnumerator NetworkService_Request_Success()
        {
            var url = "www.google.es";
            _networkRequestModel = new NetworkRequestModel(url);
            _networkService.Request(_networkRequestModel, OnRequestFinished);
            
            yield return new WaitUntil(() => _networkRequestModel.Result != UnityWebRequest.Result.InProgress);
            
            Assert.That(_requestStatus, Is.EqualTo(UnityWebRequest.Result.Success));
        }

        [UnityTest]
        public IEnumerator NetworkService_Request_Failed()
        {
            var url = "www.google.eo";
            _networkRequestModel = new NetworkRequestModel(url);
            _networkService.Request(_networkRequestModel, OnRequestFinished);

            yield return new WaitUntil(() => _networkRequestModel.Result != UnityWebRequest.Result.InProgress);

            Assert.That(_requestStatus, Is.EqualTo(UnityWebRequest.Result.ConnectionError));
        }

        private void OnRequestFinished(NetworkRequestModel networkRequestModel)
        {
            _requestStatus = networkRequestModel.Result;
        }
    }
}