using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace ContrRendaFixa
{
    public class TratamentoErrosMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TratamentoErrosMiddleware> _logger;

        public TratamentoErrosMiddleware(RequestDelegate next, ILogger<TratamentoErrosMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Um erro inesperado ocorreu.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var errorDetails = new
            {
                error = new[]
                {
                    new
                    {
                        code = "APPERRORCODE0000",
                        message = "Ocorreu um erro inesperado" //,
                        //detail = exception.Message,
                        //stackTrace = exception.StackTrace
                    }
                }
            };

            var result = JsonSerializer.Serialize(errorDetails);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);
        }
    }
}
