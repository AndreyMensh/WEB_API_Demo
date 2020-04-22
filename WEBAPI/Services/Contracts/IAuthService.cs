using System.Net;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.ViewModels.Auth;

namespace WEBAPI.Services.Contracts
{
    public interface IAuthService
    {
        TokenViewModel TokenMobile(GetTokenViewModel model);
        TokenViewModel Token(GetTokenViewModel model, IPAddress ipAddress);
        TokenViewModel Refresh(RefreshTokenViewModel model);
        TokenViewModel GetToken(User user);
        void RestorePassword(RestorePasswordViewModel model);

        TokenViewModel ConfirmIp(string username, IPAddress remoteIp, string code);

        bool IsBlocked(int companyId);
        void LogOutCompany(int companyId);
    }
}
