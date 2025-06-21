using System.ComponentModel.DataAnnotations;

namespace HelperPE.Common.Models.Auth
{
    public class LoginUserModel
    {
        [EmailAddress, Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
