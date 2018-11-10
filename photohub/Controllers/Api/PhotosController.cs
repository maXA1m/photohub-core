using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.Extensions;
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

        private bool _isDisposed;

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
            return _photosService.GetAll(page, _getAllPageSize).ToViewModels();
        }
        
        [HttpGet, Route("details/{id}")]
        public async Task<PhotoViewModel> Get(int id)
        {
            return (await _photosService.GetAsync(id)).ToViewModel();
        }
        
        [HttpGet, Route("home/{page}")]
        public IEnumerable<PhotoViewModel> GetPhotosHome(int page)
        {
            return _photosService.GetPhotosHome(page, _getHomePageSize).ToViewModels();
        }
        
        [HttpGet, Route("user/{userName}/{page}")]
        public IEnumerable<PhotoViewModel> GetForUser(int page, string userName)
        {
            return _photosService.GetForUser(page, userName, _getForUserPageSize).ToViewModels();
        }

        [HttpGet, Route("tag/{tagName}")]
        public IEnumerable<PhotoViewModel> GetForTag(string tagName)
        {
            return _photosService.GetForTag(tagName, _getForTagPageSize).ToViewModels();
        }

        [HttpGet, Route("bookmarks/{page}")]
        public IEnumerable<PhotoViewModel> GetBookmarks(int page)
        {
            return _photosService.GetBookmarks(page, _getAllPageSize).ToViewModels();
        }

        [HttpGet, Route("tags/{tagId}/{page}")]
        public IEnumerable<PhotoViewModel> GetTags(int tagId, int page)
        {
            return _photosService.GetTags(tagId, page, _getAllPageSize).ToViewModels();
        }

        [HttpGet, Route("search")]
        public IEnumerable<PhotoViewModel> Search(int page, string search, int? iso, double? exposure, double? aperture, double? focalLength)
        {
            return _photosService.Search(page, search, _getHomePageSize, iso, exposure, aperture, focalLength).ToViewModels();
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
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _photosService.Dispose();
                }

                _isDisposed = true;

                base.Dispose(disposing);
            }
        }

        #endregion
    }
}
