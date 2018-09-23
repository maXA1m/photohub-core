using System.Threading.Tasks;
using PhotoHub.BLL.Interfaces;

namespace PhotoHub.BLL.Services
{
    /// <summary>
    /// This class is used by the application to send email for account confirmation and password reset.
    /// For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    /// </summary>
    public class EmailSender : IEmailSender
    {
        #region Logic

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}
