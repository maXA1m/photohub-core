using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.Mappers;
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Controllers.Api
{
    [Route("api/giveaways")]
    public class GiveawaysController : Controller
    {
        private readonly IGiveawaysService _giveawaysService;

        private const int _getAllPageSize = 8;
        private const int _getForUserPageSize = 4;

        public GiveawaysController(IGiveawaysService giveawaysService)
        {
            _giveawaysService = giveawaysService;
        }

        [HttpGet, Route("{page}")]
        public async Task<List<GiveawayViewModel>> GetAll(int page)
        {
            return GiveawaysDTOMapper.ToGiveawayViewModels(await _giveawaysService.GetAllAsync(page, _getAllPageSize));
        }
        
        [HttpGet, Route("details/{id}")]
        public async Task<GiveawayDetailsViewModel> Get(int id)
        {
            return GiveawaysDTOMapper.ToGiveawayDetailsViewModel(await _giveawaysService.GetAsync(id));
        }
        
        [HttpGet, Route("{userName}/{page}")]
        public List<GiveawayViewModel> GetForUser(int page, string userName)
        {
            return GiveawaysDTOMapper.ToGiveawayViewModels(_giveawaysService.GetForUser(page, userName, _getForUserPageSize));
        }

        protected override void Dispose(bool disposing)
        {
            _giveawaysService.Dispose();
            base.Dispose(disposing);
        }
    }
}