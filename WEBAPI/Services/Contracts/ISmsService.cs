using WEBAPI.ViewModels.Sms;

namespace WEBAPI.Services.Contracts
{
    public interface ISmsService
    {
        void SendSms(SendSmsViewModel model);
    }
}
