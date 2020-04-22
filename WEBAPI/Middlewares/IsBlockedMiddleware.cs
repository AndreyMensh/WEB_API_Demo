using System.Linq;
using System.Threading.Tasks;
using Helpers.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WEBAPI.Model;
using WEBAPI.Services.Contracts;

namespace WEBAPI.Middlewares
{
    public class IsBlockedMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHelperService _helperService;
        private readonly IAuthService _authService;

        public IsBlockedMiddleware(RequestDelegate next, IHelperService helperService, IAuthService authService)
        {
            _next = next;
            _helperService = helperService;
            _authService = authService;
 
        }

        public async Task InvokeAsync(HttpContext context)
        {
 

            var companyId = _helperService.GetCompanyId(context.User);
            var isBlocked = _authService.IsBlocked(companyId);

            if (isBlocked)
            {
                context.Response.StatusCode = 401;
            }

            await _next.Invoke(context);
        }
    }
}
