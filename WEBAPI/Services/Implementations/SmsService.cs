using Twilio;
using Twilio.Rest.Api.V2010.Account;
using WEBAPI.Services.Contracts;
using Microsoft.Extensions.Configuration;
using WEBAPI.ViewModels.Sms;

namespace WEBAPI.Services.Implementations
{
    public class SmsService : ISmsService
    {
        public IConfiguration Configuration { get; }

        public SmsService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void SendSms(SendSmsViewModel model)
        {
            TwilioClient.Init(Configuration["Twilio:accountSid"], Configuration["Twilio:authToken"]);

            var smsMessage = MessageResource.Create(
                body: model.Message,
                from: new Twilio.Types.PhoneNumber(Configuration["Twilio:phoneNumber"]),
                to: new Twilio.Types.PhoneNumber(model.PhoneNumber)
            );
        }
    }
}
