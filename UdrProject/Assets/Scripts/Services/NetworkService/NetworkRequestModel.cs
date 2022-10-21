using System;
using System.Diagnostics.Contracts;
using UnityEngine.Networking;

namespace Urd.Services.Network
{
    public class NetworkRequestModel
	{
        public string Url { get; private set; }
        public NetworkRequestType RequestType { get; private set; }
		public string PostData { get; private set; }
        public byte[] PutData { get; private set; }
        public bool UseCache { get; private set; }

        private string _responseData;

        public UnityWebRequest.Result Result { get; private set; }
        public string ErrorMessage { get; private set; }


        public NetworkRequestModel(string url) : this(url, NetworkRequestType.Get) { }
        public NetworkRequestModel(string url, NetworkRequestType networkRequestType) : this(url, networkRequestType, null) { }
        public NetworkRequestModel(string url, string postData): this(url, NetworkRequestType.Post, postData:postData) { }
        public NetworkRequestModel(string url, byte[] putData) : this(url, NetworkRequestType.Put, putData: putData) { }

        public NetworkRequestModel(string url, NetworkRequestType requestType, string postData = null, byte[] putData = null)
		{
            Contract.Assert(url?.Length > 0, "[NetworkRequestModel] the url is null or empty");

            Url = url;
			RequestType = requestType;
			PostData = postData;
            PutData = putData;
         }

        public void SetResponseData(string responseData)
        {
            Result = UnityWebRequest.Result.Success;
            _responseData = responseData;
        }

        internal void SetErrorResponse(string errorMessage, UnityWebRequest.Result errorType)
        {
            Result = errorType;
            ErrorMessage = errorMessage;
        }
    }
}
