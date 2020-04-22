using System.ComponentModel.DataAnnotations;

namespace WEBAPI.ViewModels.Auth
{
    public class RefreshTokenViewModel
    {
        [Required]
        public string RefreshToken { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
