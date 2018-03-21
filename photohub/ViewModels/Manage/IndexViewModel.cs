using System.ComponentModel.DataAnnotations;

namespace PhotoHub.WEB.ViewModels.Manage
{
    public class IndexViewModel
    {
        [Display(Name = "Real name")]
        public string RealName { get; set; }
        public string Username { get; set; }
        public string About { get; set; }
        public bool IsEmailConfirmed { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Phone, Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        public string StatusMessage { get; set; }
    }
}
