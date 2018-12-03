using Abp.ObjectMapping;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TemplateName.WebApi.Exceptions
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        [System.Diagnostics.DebuggerHidden]
        public async Task InvokeAsync(HttpContext context, ILoggerFactory loggerFactory, IObjectMapper objectMapper)
        {
            try
            {
                await next(context);
            }
            catch (WebApiException ex)
            {
                await HandleExceptionAsync(context, ex, loggerFactory, objectMapper);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, WebApiException exception, ILoggerFactory loggerFactory, IObjectMapper objectMapper)
        {
            var logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();
            logger.LogError(exception, exception.Message);

            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (exception.InnerException is NotImplementedException)
                code = HttpStatusCode.NotImplemented;

            var result = JsonConvert.SerializeObject(objectMapper.Map<ExceptionModel>(exception));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
