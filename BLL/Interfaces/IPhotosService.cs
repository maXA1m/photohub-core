using PhotoHub.BLL.DTO;
using PhotoHub.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoHub.BLL.Interfaces
{
    public interface IPhotosService : IDisposable, IReturnUser
    {
        List<FilterDTO> Filters { get; }

        IEnumerable<PhotoDTO> GetAll(int page, int pageSize);
        Task<IEnumerable<PhotoDTO>> GetAllAsync(int page, int pageSize);

        PhotoDTO Get(int id);
        Task<PhotoDTO> GetAsync(int id);

        IEnumerable<PhotoDTO> GetPhotosHome(int page, int pageSize);

        IEnumerable<PhotoDTO> GetForUser(int page, string userName, int pageSize);

        IEnumerable<PhotoDTO> GetForGiveaway(int page, int giveawayId, int pageSize);

        void RemoveFromGiveaway(int id);
        Task RemoveFromGiveawayAsync(int id);

        void AddToGiveaway(int giveawayId, int id);
        Task AddToGiveawayAsync(int giveawayId, int id);

        int Create(string filter, string description, string path);
        Task<int> CreateAsync(string filter, string description, string path);

        void Edit(int id, string filter, string description);
        Task EditAsync(int id, string filter, string description);

        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
