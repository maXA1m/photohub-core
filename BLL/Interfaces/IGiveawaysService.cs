using PhotoHub.BLL.DTO;
using PhotoHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoHub.BLL.Interfaces
{
    public interface IGiveawaysService : IDisposable, IReturnUser
    {
        IEnumerable<GiveawayDTO> GetAll(int page, int pageSize);
        Task<IEnumerable<GiveawayDTO>> GetAllAsync(int page, int pageSize);

        GiveawayDetailsDTO Get(int id);
        Task<GiveawayDetailsDTO> GetAsync(int id);

        IEnumerable<GiveawayDTO> GetForUser(int page, string userName, int pageSize);

        int Create(string name, string email, string about, string avatar);
        Task<int> CreateAsync(string name, string email, string about, string avatar);

        void Edit(int id, string name, string email, string about, string avatar);
        Task EditAsync(int id, string name, string email, string about, string avatar);

        void Enter(int id);
        Task EnterAsync(int id);

        void Leave(int id);
        Task LeaveAsync(int id);

        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
