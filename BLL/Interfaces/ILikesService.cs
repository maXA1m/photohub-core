#region using System
using System;
using System.Threading.Tasks;
#endregion

namespace PhotoHub.BLL.Interfaces
{
    public interface ILikesService : IDisposable
    {
        void Add(int photoId);
        Task AddAsync(int photoId);

        void Delete(int photoId);
        Task DeleteAsync(int photoId);
    }
}
