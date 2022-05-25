using System.Collections.Generic;

namespace Domain.OperationResponses
{
    public class DefaultErrorResponse
    {
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public List<ValidationError> Errors { get; set; }

        public DefaultErrorResponse()
        {
        }

        public DefaultErrorResponse(string message)
        {
            Message = message;
        }

        public DefaultErrorResponse(string message, string errorCode)
        {
            Message = message;
            ErrorCode = errorCode;
        }
    }

    public class ValidationError
    {
        public string Field { get; set; }
        public string Message { get; set; }
    }
}
