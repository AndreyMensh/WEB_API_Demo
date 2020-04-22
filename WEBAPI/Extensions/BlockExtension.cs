using Microsoft.AspNetCore.Builder;
using WEBAPI.Middlewares;

namespace WEBAPI.Extensions
{
    public static class BlockExtension
    {
        public static IApplicationBuilder UseBlockChecker(this IApplicationBuilder app)
        {
            return app.UseMiddleware<IsBlockedMiddleware>();
        }
    }
}
