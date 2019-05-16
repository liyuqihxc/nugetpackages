using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Abp.ObjectMapping;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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
        public async Task InvokeAsync(HttpContext context, ILoggerFactory loggerFactory, IObjectMapper objectMapper, IOptions<MvcJsonOptions> jsonOption)
        {
            try
            {
                await next(context);
            }
            catch (WebApiException ex)
            {
                await HandleExceptionAsync(context, ex, loggerFactory, objectMapper, jsonOption);
            }
        }

        private Dictionary<Type, int> _statusCodeMap = new Dictionary<Type, int>
        { 
            { typeof(NotImplementedException), StatusCodes.Status501NotImplemented },
            { typeof(KeyNotFoundException), StatusCodes.Status404NotFound },
            { typeof(IndexOutOfRangeException), StatusCodes.Status404NotFound },
            { typeof(ArgumentOutOfRangeException), StatusCodes.Status400BadRequest },
            { typeof(ArgumentException), StatusCodes.Status400BadRequest }
        };

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception,
            ILoggerFactory loggerFactory,
            IObjectMapper objectMapper,
            IOptions<MvcJsonOptions> jsonOption
        )
        {
            var logger = loggerFactory.CreateLogger<ErrorHandlingMiddleware>();
            logger.LogError(exception, exception.Message);

            var code = StatusCodes.Status500InternalServerError; // 500 if unexpected
            if (_statusCodeMap.ContainsKey(exception.GetType()))
            {
                code = _statusCodeMap[exception.GetType()];
            }

            // if (typeof(ArgumentException).IsAssignableFrom(exception.GetType()))
            // {
            // ArgumentException || ArgumentNullException || ArgumentOutOfRangeException
            // code = HttpStatusCode.BadRequest;
            // }

            var result = JsonConvert.SerializeObject(new ExceptionModel(exception), jsonOption.Value.SerializerSettings);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            await context.Response.WriteAsync(result);
        }
    }
}
