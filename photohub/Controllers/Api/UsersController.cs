using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.Mappers;
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Controllers.Api
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        #region Fields

        private readonly IUsersService _usersService;

        private const int _getAllPageSize = 8;
        private const int _getSearchPageSize = 12;

        #endregion

        #region .ctors

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        #endregion

        #region Logic

        [HttpGet, Route("{page}")]
        public IEnumerable<UserViewModel> GetAll(int page)
        {
            return UsersMapper.MapRange(_usersService.GetAll(page, _getAllPageSize));
        }
        
        [HttpGet, Route("details/{userName}")]
        public UserDetailsViewModel Get(string userName)
        {
            return UsersDetailsMapper.Map(_usersService.Get(userName));
        }


        [HttpGet, Route("blocklist/{page}")]
        public IEnumerable<UserViewModel> GetBlacklist(int page)
        {
            return UsersMapper.MapRange(_usersService.GetBlocked(page, _getSearchPageSize));
        }

        [HttpGet, Route("search")]
        public IEnumerable<UserViewModel> Search(int page, string search)
        {
            return UsersMapper.MapRange(_usersService.Search(page, search, _getSearchPageSize));
        }
        
        [Authorize, HttpPost, Route("follow/{follow}")]
        public async Task Follow(string follow)
        {
            await _usersService.FollowAsync(follow);
        }
        
        [Authorize, HttpPost, Route("dismiss/follow/{follow}")]
        public async Task DismissFollow(string follow)
        {
            await _usersService.DismissFollowAsync(follow);
        }
        
        [Authorize, HttpPost, Route("block/{block}")]
        public async Task Block(string block)
        {
            await _usersService.BlockAsync(block);
        }
        
        [Authorize, HttpPost, Route("dismiss/block/{block}")]
        public async Task DismissBlock(string block)
        {
            await _usersService.DismissBlockAsync(block);
        }

        [Authorize, HttpPost, Route("report/{report}")]
        public async Task Report(string report, string text)
        {
            await _usersService.ReportAsync(report, text);
        }

        #endregion

        #region Disposing

        protected override void Dispose(bool disposing)
        {
            _usersService.Dispose();
            base.Dispose(disposing);
        }

        #endregion
    }
}
