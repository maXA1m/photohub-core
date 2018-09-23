using System.Threading.Tasks;

namespace PhotoHub.BLL.Interfaces
{
    /// <summary>
    /// Interface for email sernder services.
    /// </summary>
    public interface IEmailSender
    {
        #region Methods

        /// <summary>
        /// Async sends email.
        /// </summary>
        Task SendEmailAsync(string email, string subject, string message);

        #endregion
    }
}
