#region using System/Microsoft
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
#endregion
using PhotoHub.BLL.Interfaces;
#region using PhotoHub.WEB
using PhotoHub.WEB.Mappers;
using PhotoHub.WEB.ViewModels;
#endregion

namespace PhotoHub.WEB.Controllers.Api
{
    [Route("api/photos")]
    public class PhotosController : Controller
    {
        private readonly IPhotosService _photosService;
        private readonly PhotosMapper _photosMapper;

        private const int _getAllPageSize = 8;
        private const int _getHomePageSize = 8;
        private const int _getForUserPageSize = 4;

        public PhotosController(IPhotosService photosService)
        {
            _photosService = photosService;
            _photosMapper = new PhotosMapper();
        }

        [HttpGet, Route("{page}")]
        public IEnumerable<PhotoViewModel> GetAll(int page)
        {
            return _photosMapper.MapRange(_photosService.GetAll(page, _getAllPageSize));
        }
        
        [HttpGet, Route("details/{id}")]
        public async Task<PhotoViewModel> Get(int id)
        {
            return _photosMapper.Map(await _photosService.GetAsync(id));
        }
        
        [HttpGet, Route("home/{page}")]
        public IEnumerable<PhotoViewModel> GetPhotosHome(int page)
        {
            return _photosMapper.MapRange(_photosService.GetPhotosHome(page, _getHomePageSize));
        }
        
        [HttpGet, Route("user/{userName}/{page}")]
        public IEnumerable<PhotoViewModel> GetForUser(int page, string userName)
        {
            return _photosMapper.MapRange(_photosService.GetForUser(page, userName, _getForUserPageSize));
        }

        [HttpGet, Route("bookmarks/{page}")]
        public IEnumerable<PhotoViewModel> GetBookmarks(int page)
        {
            return _photosMapper.MapRange(_photosService.GetBookmarks(page, _getForUserPageSize));
        }

        [HttpGet, Route("tags/{tagId}/{page}")]
        public IEnumerable<PhotoViewModel> GetTags(int tagId, int page)
        {
            return _photosMapper.MapRange(_photosService.GetTags(tagId, page, _getAllPageSize));
        }

        [HttpGet, Route("search")]
        public IEnumerable<PhotoViewModel> Search(int page, string search, int iso, int exposure, int aperture, double focalLength)
        {
            return _photosMapper.MapRange(_photosService.Search(page, search, _getHomePageSize, iso, exposure, aperture, focalLength));
        }

        [Authorize, HttpPost, Route("bookmark/{id}")]
        public async Task Bookmark(int id)
        {
            await _photosService.BookmarkAsync(id);
        }

        [Authorize, HttpPost, Route("dismiss/bookmark/{id}")]
        public async Task DismissBookmark(int id)
        {
            await _photosService.DismissBookmarkAsync(id);
        }

        protected override void Dispose(bool disposing)
        {
            _photosService.Dispose();
            base.Dispose(disposing);
        }
    }
}
