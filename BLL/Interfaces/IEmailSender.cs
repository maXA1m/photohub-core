using System.Threading.Tasks;

namespace PhotoHub.BLL.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
