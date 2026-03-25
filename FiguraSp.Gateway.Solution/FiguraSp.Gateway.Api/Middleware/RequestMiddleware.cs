namespace FiguraSp.Gateway.Api.Middleware
{
    public class RequestMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.Headers["Figura-Gateway"] = "Signed";
            await next(context);
        }
    }
}
