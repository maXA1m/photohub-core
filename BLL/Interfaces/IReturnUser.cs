using PhotoHub.BLL.DTO;
using PhotoHub.DAL.Entities;

namespace PhotoHub.BLL.Interfaces
{
    public interface IReturnUser
    {
        ApplicationUser CurrentUser { get; }
        UserDTO CurrentUserDTO { get; }
    }
}
