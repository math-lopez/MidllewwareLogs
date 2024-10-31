using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System;

namespace MeuMiddleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _serviceId;

        public LoggingMiddleware(RequestDelegate next, string serviceId)
        {
            _next = next;
            _serviceId = serviceId;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log antes de processar a solicitação
            var logEntrada = new
            {
                ServiceId = _serviceId,
                LogType = "Request",
                Method = context.Request.Method,
                Path = context.Request.Path,
                Timestamp = DateTime.UtcNow
            };
            Console.WriteLine(JsonSerializer.Serialize(logEntrada));

            await _next(context);

            // Log após processar a solicitação
            var logSaida = new
            {
                ServiceId = _serviceId,
                LogType = "Response",
                StatusCode = context.Response.StatusCode,
                Timestamp = DateTime.UtcNow
            };
            Console.WriteLine(JsonSerializer.Serialize(logSaida));
        }
    }
}
