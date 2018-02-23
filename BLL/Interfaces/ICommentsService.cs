using PhotoHub.BLL.DTO;
using PhotoHub.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace PhotoHub.BLL.Interfaces
{
    public interface ICommentsService : IDisposable, IReturnUser
    {
        int? Add(int photoId, string text);
        Task<int?> AddAsync(int photoId, string text);

        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
