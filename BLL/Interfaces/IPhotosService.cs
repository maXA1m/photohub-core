#region using System
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion
using PhotoHub.BLL.DTO;

namespace PhotoHub.BLL.Interfaces
{
    public interface IPhotosService : IDisposable, IReturnUser
    {
        List<FilterDTO> Filters { get; }
        List<TagDTO> Tags { get; }

        IEnumerable<PhotoDTO> GetAll(int page, int pageSize);

        PhotoDTO Get(int id);
        Task<PhotoDTO> GetAsync(int id);

        IEnumerable<PhotoDTO> GetPhotosHome(int page, int pageSize);

        IEnumerable<PhotoDTO> GetForUser(int page, string userName, int pageSize);

        IEnumerable<PhotoDTO> GetBookmarks(int page, int pageSize);

        IEnumerable<PhotoDTO> GetTags(int tagId, int page, int pageSize);

        IEnumerable<PhotoDTO> Search(int page, string search, int pageSize, int? iso, double? exposure, double? aperture, double? focalLength);

        void Bookmark(int id);
        Task BookmarkAsync(int id);

        void DismissBookmark(int id);
        Task DismissBookmarkAsync(int id);

        int Create(string filter, string description, string path, string manufacturer, string model, int? iso, double? exposure, double? aperture, double? focalLength, string tags);
        ValueTask<int> CreateAsync(string filter, string description, string path, string manufacturer, string model, int? iso, double? exposure, double? aperture, double? focalLength, string tags);

        void Edit(int id, string filter, string description, string tags, string model, string brand, int? iso, double? aperture, double? exposure, double? focalLength);
        Task EditAsync(int id, string filter, string description, string tags, string model, string brand, int? iso, double? aperture, double? exposure, double? focalLength);

        void Delete(int id);
        Task DeleteAsync(int id);
    }
}
