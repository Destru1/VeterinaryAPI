using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VeterinaryAPI.Common;
using VeterinaryAPI.Common.Exeptions;

namespace VeterinaryAPI.Infastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            ErrorDetails errorDetails = new ErrorDetails
            {
                Message = exception.Message,
            };

            switch (exception)
            {
                case ModelException:
                    ModelException modelException = exception as ModelException;
                    errorDetails.Message = modelException.ErrorMessage.Select(e => e.ErrorMessage);
                    break;
                case EntityDoesNotExistException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

            }

            errorDetails.StatusCode = context.Response.StatusCode;

            string result = errorDetails.ToString();
            await context.Response.WriteAsync(result);
        }
    }
}

