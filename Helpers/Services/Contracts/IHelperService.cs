using System.Security.Claims;

namespace Helpers.Services.Contracts
{
    public interface IHelperService
    {
        int GetUserId(ClaimsPrincipal claimsPrincipal);
        string GetRole(ClaimsPrincipal claimsPrincipal);
        int GetCompanyId(ClaimsPrincipal claimsPrincipal);
    }
}
