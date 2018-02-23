using PhotoHub.BLL.DTO;
using PhotoHub.DAL.Entities;
using System;
using System.Threading.Tasks;

namespace PhotoHub.BLL.Interfaces
{
    public interface ILikesService : IDisposable, IReturnUser
    {
        void Add(int photoId);
        Task AddAsync(int photoId);

        void Delete(int photoId);
        Task DeleteAsync(int photoId);
    }
}
