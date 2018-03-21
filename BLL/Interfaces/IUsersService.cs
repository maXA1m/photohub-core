using PhotoHub.BLL.DTO;
using PhotoHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoHub.BLL.Interfaces
{
    public interface IUsersService : IDisposable, IReturnUser
    {
        IEnumerable<UserDTO> GetAll(int page, int pageSize);

        UserDetailsDTO Get(string userName);

        IEnumerable<UserDTO> GetBlocked(int page, int pageSize);

        IEnumerable<UserDTO> Search(int page, string search, int pageSize);

        void Follow(string follow);
        Task FollowAsync(string follow);

        void DismissFollow(string follow);
        Task DismissFollowAsync(string follow);

        void Block(string block);
        Task BlockAsync(string block);

        void DismissBlock(string block);
        Task DismissBlockAsync(string block);

        ApplicationUser Create(string userName, string email, string password);
        Task<ApplicationUser> CreateAsync(string userName, string email, string password);

        void Edit(string userName, string realName, string phoneNumber, string email, string about);
        Task EditAsync(string userName, string realName, string phoneNumber, string email, string about);
    }
}
