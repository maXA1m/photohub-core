using System.Text.Encodings.Web;
using System.Threading.Tasks;
using PhotoHub.BLL.Interfaces;

namespace PhotoHub.WEB.Extensions
{
    public static class EmailSenderExtensions
    {
        #region Logic

        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }

        #endregion
    }
}
