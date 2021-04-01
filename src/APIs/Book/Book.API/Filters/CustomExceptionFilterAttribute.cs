using Book.API.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

using System;
using System.Net;
using System.Net.Http;

namespace Book.API.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilterAttribute> _logger;

        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);

            var statusCode = HttpStatusCode.BadRequest;

            //Status code can be custom made
            var errorResponseDTO = new ErrorResponseDTO
            {
                Message = context.Exception.Message,
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            if (context.Exception is HttpRequestException)
            {
                statusCode = HttpStatusCode.BadRequest;
                errorResponseDTO.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;
            context.Result = new JsonResult(errorResponseDTO);
        }
    }
}
