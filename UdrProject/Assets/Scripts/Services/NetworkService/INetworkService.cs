using System;
using Urd.Error;
using Urd.Services.Network;

namespace Urd.Services
{
    public interface INetworkService : IBaseService
    {
        void Request(NetworkRequestModel networkRequestModel, Action<NetworkRequestModel> onRequestHttpFinishedSuccess, Action<ErrorModel> onRequestHttpFinishedFailed);
    }
}