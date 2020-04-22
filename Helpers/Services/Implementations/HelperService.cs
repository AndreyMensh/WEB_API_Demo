using System.Linq;
using System.Security.Claims;
using Helpers.Services.Contracts;

namespace Helpers.Services.Implementations
{
    public class HelperService : IHelperService
    {
        public int GetUserId(ClaimsPrincipal claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value);
        }

        public string GetRole(ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;
        }

        public int GetCompanyId(ClaimsPrincipal claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "CompanyId")?.Value);
        }
    }
}
