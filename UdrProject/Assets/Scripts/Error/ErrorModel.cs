using System;
using System.Diagnostics.Contracts;

namespace Urd.Error
{
    public class ErrorModel
    {
        private Exception exception;

        public string Message { get; private set; }
        public ErrorCode ErrorCode { get; private set; }

        public ErrorModel(Exception exception) : this(exception.Message, ErrorCode.Error_1000_Exception_Error) { }
        public ErrorModel(Exception exception, ErrorCode errorCode) : this(exception.Message, errorCode) { }
        public ErrorModel(string message, long responseCode) : this(message, (ErrorCode)responseCode) { }
        public ErrorModel(string message, ErrorCode errorCode)
        {
            Contract.Assert(message?.Length > 0, "[ErrorModel] the key is null or empty");

            Message = message;
            ErrorCode = errorCode;
        }

    }
}