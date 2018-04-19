using System.ComponentModel.DataAnnotations;

namespace PhotoHub.WEB.ViewModels.Account
{
    public class ExternalLoginViewModel
    {
        [Required]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
