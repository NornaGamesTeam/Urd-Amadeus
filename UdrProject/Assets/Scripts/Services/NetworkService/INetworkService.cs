using System;
using Urd.Services.Network;

namespace Urd.Services
{
    public interface INetworkService : IBaseService
    {
        void Request(NetworkRequestModel networkRequestModel, Action<NetworkRequestModel> onRequestHttpFinished);
    }
}