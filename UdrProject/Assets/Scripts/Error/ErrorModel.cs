using System;
using System.Diagnostics.Contracts;
using static UnityEngine.Networking.UnityWebRequest;

namespace Urd.Error
{
    public class ErrorModel
    {
        private Exception exception;

        public string Message { get; private set; }
        public ErrorCode ErrorCode { get; private set; }
        public Result ConectionResult { get; private set; }

        public ErrorModel(Exception exception) : this(exception.Message, ErrorCode.Error_1000_Exception_Error, Result.ConnectionError) { }
        public ErrorModel(Exception exception, ErrorCode errorCode, Result conectionResult = Result.ConnectionError) : this(exception.Message, errorCode, conectionResult) { }
        public ErrorModel(string message, long responseCode, Result conectionResult) : this(message, (ErrorCode)responseCode, conectionResult) { }
        public ErrorModel(string message, ErrorCode errorCode, Result conectionResult)
        {
            Contract.Assert(message?.Length > 0, "[ErrorModel] the key is null or empty");

            Message = message;
            ErrorCode = errorCode;
            ConectionResult = conectionResult;
        }

        public override string ToString()
        {
            return $"Error: {Message}. ErrorCode: {ErrorCode}";
        }
    }
}