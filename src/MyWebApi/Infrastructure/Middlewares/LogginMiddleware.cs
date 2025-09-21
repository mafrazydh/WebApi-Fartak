using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Middlewares
{
    public class LogginMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LogginMiddleware(RequestDelegate next, ILogger<LogginMiddleware> logger)
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
            finally
            {
                _logger?.LogInformation($"{context.Request.Method} {context.Request.Path} => {context.Response.StatusCode}{Environment.NewLine}");
            }
        }
    }
}
