using Infrastructure.Middlewares;

namespace MyWebApi.Infrastructure.Middlewares
{
    public static class LogginMiddlewareExtenstion
    {
        public static WebApplication UseLogging(this WebApplication app)
        {
            app.UseMiddleware<LogginMiddleware>();
            return app;
        }
    }
}
