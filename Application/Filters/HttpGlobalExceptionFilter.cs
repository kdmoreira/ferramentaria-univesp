using Domain.Enums;
using Domain.Extensions;
using Domain.OperationResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Domain.Exceptions;
using System;
using System.Net;

namespace Application.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

            HttpResponse response = context.HttpContext.Response;
            DefaultErrorResponse defaultErrorResponseModel = new DefaultErrorResponse($"{context.Exception.Message}");

            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            defaultErrorResponseModel.ErrorCode = Enum.GetName(ErrorCodeEnum.InternalServerError)?.ToSnakeCase().ToLower();

            if (context.Exception is FerramentariaDomainException exception)
            {
                defaultErrorResponseModel.ErrorCode = Enum.GetName(exception.ErrorCode)?.ToSnakeCase().ToLower();
                response.StatusCode = (int)HttpStatusCode.BadRequest;

                if (exception.StatusCode.HasValue)
                {
                    response.StatusCode = (int)exception.StatusCode;
                }
            }

            response.ContentType = "application/json";

            context.ExceptionHandled = true;
            context.Result = new ObjectResult(defaultErrorResponseModel);
        }
    }
}