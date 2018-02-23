using System.ComponentModel.DataAnnotations;

namespace PhotoHub.WEB.ViewModels.Account
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
