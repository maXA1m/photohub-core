using System.ComponentModel.DataAnnotations;

namespace PhotoHub.WEB.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
