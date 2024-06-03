using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using HistoricalNewsUpdate.Common.Exceptions;
using HistoricalNewsUpdate.Common.Models;
using System.Net;

namespace HistoricalNewsUpdate.Extensions
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
            var exception = context.Exception;
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }
            if (context.ModelState.ErrorCount > 0)
            {
                var errors = context.ModelState.Where(v => v.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => string.IsNullOrEmpty(kvp.Key) ? kvp.Key : $"{char.ToLower(kvp.Key[0])}{kvp.Key.Substring(1)}",
                        kvp => kvp.Value.Errors.FirstOrDefault()?.ErrorMessage
                    );

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                context.Result = new UnprocessableEntityObjectResult(new JsonResponse
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                    Data = errors
                });
                context.ExceptionHandled = true;
                return;
            }

            var json = new JsonResponse
            {
                Message = context.Exception.Message
            };

            // 400 Bad Request
            if (context.Exception.GetType() == typeof(AppException))
            {
                var errorCode = (int?)exception.Data[AppException.ErrorCode];
                if (errorCode != null)
                {
                    json.StatusCode = errorCode.Value;
                    context.HttpContext.Response.StatusCode = errorCode.Value;
                }
                else
                {
                    json.StatusCode = StatusCodes.Status400BadRequest;
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
                context.Result = new BadRequestObjectResult(json);
            }
            // 404 Not Found
            else if (context.Exception.GetType() == typeof(NotFoundException))
            {
                json.StatusCode = StatusCodes.Status404NotFound;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Result = new NotFoundObjectResult(json);
            }
            // 500 Internal Server Error
            else
            {
                json.Message = exception.Message;
                json.StatusCode = StatusCodes.Status500InternalServerError;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new InternalServerErrorObjectResult(json);
                _logger.LogError(context.Exception.ToString());
            }
            context.ExceptionHandled = true;
        }
    }
}
