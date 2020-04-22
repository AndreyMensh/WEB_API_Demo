using System.ComponentModel.DataAnnotations;

namespace WEBAPI.ViewModels.Sms
{
    public class SendSmsViewModel
    {
        [Required]
        [StringLength(20)]
        public string Message { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
