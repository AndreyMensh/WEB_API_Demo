using System.Threading.Tasks;

namespace Helpers.Services.Contracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
