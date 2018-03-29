using PhotoHub.DAL.Entities;
using PhotoHub.BLL.DTO;

namespace PhotoHub.BLL.Interfaces
{
    public interface IReturnUser
    {
        ApplicationUser CurrentUser { get; }
        UserDTO CurrentUserDTO { get; }
    }
}
