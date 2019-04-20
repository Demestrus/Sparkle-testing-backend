using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SparkleTesting.Application.Exceptions;
using System;
using System.Threading.Tasks;
using Serilog;

namespace SparkleTesting.API.Middleware
{
    public class ExceptionHandling
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _env;
        private readonly ILogger _log;

        public ExceptionHandling(RequestDelegate next, IHostingEnvironment env, ILogger log)
        {
            _next = next;
            _env = env;
            _log = log;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var result = JsonConvert.SerializeObject(new
            {
                error =
                    exception.InnerException == null
                        ? exception.Message
                        : $"{exception.Message.Trim('.')}: {exception.InnerException.Message}"
            });
            context.Response.ContentType = "application/json; charset=utf-8";
            switch (exception)
            {
                case NotFoundException ex:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    _log.Warning(ex, "");
                    break;
                case BadRequestException ex:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    _log.Warning(ex, "");
                    break;
                default:
                    if (_env.IsProduction())
                    {
                        result = JsonConvert.SerializeObject(new
                        {
                            error = "Произошла непредвиденная ошибка",
                        });
                    }
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    _log.Error(exception, "");
                    break;
            }
            return context.Response.WriteAsync(result);
        }
    }
}
