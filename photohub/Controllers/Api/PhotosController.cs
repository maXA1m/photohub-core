using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.Mappers;
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Controllers.Api
{
    [Route("api/photos")]
    public class PhotosController : Controller
    {
        private readonly IPhotosService _photosService;

        private const int _getAllPageSize = 8;
        private const int _getHomePageSize = 8;
        private const int _getForUserPageSize = 4;
        private const int _getForGiveawayPageSize = 4;

        public PhotosController(IPhotosService photosService)
        {
            _photosService = photosService;
        }

        [HttpGet, Route("{page}")]
        public async Task<List<PhotoViewModel>> GetAll(int page)
        {
            return PhotoDTOMapper.ToPhotoViewModels(await _photosService.GetAllAsync(page, _getAllPageSize));
        }
        
        [HttpGet, Route("details/{id}")]
        public async Task<PhotoViewModel> Get(int id)
        {
            return PhotoDTOMapper.ToPhotoViewModel(await _photosService.GetAsync(id));
        }
        
        [HttpGet, Route("home/{page}")]
        public List<PhotoViewModel> GetPhotosHome(int page)
        {
            return PhotoDTOMapper.ToPhotoViewModels(_photosService.GetPhotosHome(page, _getHomePageSize));
        }
        
        [HttpGet, Route("user/{userName}/{page}")]
        public List<PhotoViewModel> GetForUser(int page, string userName)
        {
            return PhotoDTOMapper.ToPhotoViewModels(_photosService.GetForUser(page, userName, _getForUserPageSize));
        }
        
        [HttpGet, Route("{giveawayId}/{page}")]
        public List<PhotoViewModel> GetForGiveaway(int page, int giveawayId)
        {
            return PhotoDTOMapper.ToPhotoViewModels(_photosService.GetForGiveaway(page, giveawayId, _getForGiveawayPageSize));
        }
        
        [HttpPost, Route("remove/giveaway/{id}")]
        public async Task RemoveFromGiveaway(int id)
        {
            await _photosService.RemoveFromGiveawayAsync(id);
        }

        [HttpPost, Route("add/{giveawayId}/{id}")]
        public async Task AddToGiveaway(int giveawayId, int id)
        {
            await _photosService.AddToGiveawayAsync(giveawayId, id);
        }

        protected override void Dispose(bool disposing)
        {
            _photosService.Dispose();
            base.Dispose(disposing);
        }
    }
}
