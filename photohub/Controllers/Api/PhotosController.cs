using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.Mappers;
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Controllers.Api
{
    [Route("api/photos")]
    public class PhotosController : Controller
    {
        #region Fields

        private readonly IPhotosService _photosService;

        private const int _getAllPageSize = 8;
        private const int _getHomePageSize = 8;
        private const int _getForUserPageSize = 4;
        private const int _getForTagPageSize = 16;

        #endregion

        #region .ctors

        public PhotosController(IPhotosService photosService)
        {
            _photosService = photosService;
        }

        #endregion

        #region Logic

        [HttpGet, Route("{page}")]
        public IEnumerable<PhotoViewModel> GetAll(int page)
        {
            return PhotosMapper.MapRange(_photosService.GetAll(page, _getAllPageSize));
        }
        
        [HttpGet, Route("details/{id}")]
        public async Task<PhotoViewModel> Get(int id)
        {
            return PhotosMapper.Map(await _photosService.GetAsync(id));
        }
        
        [HttpGet, Route("home/{page}")]
        public IEnumerable<PhotoViewModel> GetPhotosHome(int page)
        {
            return PhotosMapper.MapRange(_photosService.GetPhotosHome(page, _getHomePageSize));
        }
        
        [HttpGet, Route("user/{userName}/{page}")]
        public IEnumerable<PhotoViewModel> GetForUser(int page, string userName)
        {
            return PhotosMapper.MapRange(_photosService.GetForUser(page, userName, _getForUserPageSize));
        }

        [HttpGet, Route("tag/{tagName}")]
        public IEnumerable<PhotoViewModel> GetForTag(string tagName)
        {
            return PhotosMapper.MapRange(_photosService.GetForTag(tagName, _getForTagPageSize));
        }

        [HttpGet, Route("bookmarks/{page}")]
        public IEnumerable<PhotoViewModel> GetBookmarks(int page)
        {
            return PhotosMapper.MapRange(_photosService.GetBookmarks(page, _getAllPageSize));
        }

        [HttpGet, Route("tags/{tagId}/{page}")]
        public IEnumerable<PhotoViewModel> GetTags(int tagId, int page)
        {
            return PhotosMapper.MapRange(_photosService.GetTags(tagId, page, _getAllPageSize));
        }

        [HttpGet, Route("search")]
        public IEnumerable<PhotoViewModel> Search(int page, string search, int? iso, double? exposure, double? aperture, double? focalLength)
        {
            return PhotosMapper.MapRange(_photosService.Search(page, search, _getHomePageSize, iso, exposure, aperture, focalLength));
        }

        [Authorize, HttpPost, Route("bookmark/{id}")]
        public async Task Bookmark(int id)
        {
            await _photosService.BookmarkAsync(id);
        }

        [Authorize, HttpPost, Route("report/{id}")]
        public async Task Report(int id, string text)
        {
            await _photosService.ReportAsync(id, text);
        }

        [Authorize, HttpPost, Route("dismiss/bookmark/{id}")]
        public async Task DismissBookmark(int id)
        {
            await _photosService.DismissBookmarkAsync(id);
        }

        #endregion

        #region Disposing

        protected override void Dispose(bool disposing)
        {
            _photosService.Dispose();
            base.Dispose(disposing);
        }

        #endregion
    }
}
