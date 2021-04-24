using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;
using Serilog;

using OntoMath_QAS.Models.Exceptions;

using static OntoMath_QAS.AppConstants;

namespace OntoMath_QAS.Middleware
{
    public sealed class ErrorHandling
    {
        private readonly ILogger logger;

        private readonly RequestDelegate next;

        public ErrorHandling(RequestDelegate next, ILogger logger)
        {
            this.logger = logger;
            this.next   = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, logger);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            object errors = null;

            switch (exception)
            {
                case WebAppException webAppEx:
                    logger.Error(exception, "Web App error");
                    errors = webAppEx.Errors;
                    context.Response.StatusCode = (int)webAppEx.Code;
                    break;

                case Exception ex:
                    logger.Error(exception, "Server error");
                    errors = string.IsNullOrWhiteSpace(ex.Message) ? "error" : ex.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = API.ContentType.JSON;

            if (errors != null)
            {
                var result = JsonConvert.SerializeObject(new
                {
                    errors
                });

                await context.Response.WriteAsync(result);
            }
        }
    }
}