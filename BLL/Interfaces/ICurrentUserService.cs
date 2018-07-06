using System;
using PhotoHub.BLL.DTO;
using PhotoHub.DAL.Entities;

namespace PhotoHub.BLL.Interfaces
{
    public interface ICurrentUserService : IDisposable
    {
        User Get { get; }
        UserDTO GetDTO { get; }
    }
}
