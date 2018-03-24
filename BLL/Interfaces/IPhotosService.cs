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

        PhotoDTO Get(int id);
        Task<PhotoDTO> GetAsync(int id);

        IEnumerable<PhotoDTO> GetPhotosHome(int page, int pageSize);

        IEnumerable<PhotoDTO> GetForUser(int page, string userName, int pageSize);

        IEnumerable<PhotoDTO> GetBookmarks(int page, int pageSize);

        void Bookmark(int id);
        Task BookmarkAsync(int id);

        void DismissBookmark(int id);
        Task DismissBookmarkAsync(int id);

        int Create(string filter, string description, string path, string manufacturer, string model, string iso, string exposure, string aperture, string focalLength);
        ValueTask<int> CreateAsync(string filter, string description, string path, string manufacturer, string model, string iso, string exposure, string aperture, string focalLength);

        void Edit(int id, string filter, string description);
        Task EditAsync(int id, string filter, string description);

        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
