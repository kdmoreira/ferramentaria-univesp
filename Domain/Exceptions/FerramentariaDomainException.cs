using Domain.Enums;
using System;
using System.Net;

namespace Domain.Exceptions
{
    public class FerramentariaDomainException : Exception
    {
        public ErrorCodeEnum ErrorCode { get; set; }
        public HttpStatusCode? StatusCode { get; set; }

        public FerramentariaDomainException(string message, ErrorCodeEnum errorCode, HttpStatusCode? statusCode = null)
            : base(message)
        {
            this.ErrorCode = errorCode;
            this.StatusCode = statusCode;
        }

        public FerramentariaDomainException(string message, ErrorCodeEnum errorCode, Exception innerException, HttpStatusCode? statusCode = null)
            : base(message, innerException)
        {
            this.ErrorCode = errorCode;
            this.StatusCode = statusCode;
        }
    }
}
