﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PhotoHub.WEB.ViewModels;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.Mappers;

namespace PhotoHub.WEB.Controllers.Api
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly UsersDetailsMapper _usersDetailsMapper;
        private readonly UsersMapper _usersMapper;

        private const int _getAllPageSize = 8;
        private const int _getSearchPageSize = 12;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
            _usersDetailsMapper = new UsersDetailsMapper();
            _usersMapper = new UsersMapper();
        }

        [HttpGet, Route("{page}")]
        public IEnumerable<UserViewModel> GetAll(int page)
        {
            return _usersMapper.MapRange(_usersService.GetAll(page, _getAllPageSize));
        }
        
        [HttpGet, Route("details/{userName}")]
        public UserDetailsViewModel Get(string userName)
        {
            return _usersDetailsMapper.Map(_usersService.Get(userName));
        }


        [HttpGet, Route("blocklist/{page}")]
        public IEnumerable<UserViewModel> GetBlacklist(int page)
        {
            return _usersMapper.MapRange(_usersService.GetBlocked(page, _getSearchPageSize));
        }

        [HttpGet, Route("search")]
        public IEnumerable<UserViewModel> Search(int page, string search)
        {
            return _usersMapper.MapRange(_usersService.Search(page, search, _getSearchPageSize));
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

        protected override void Dispose(bool disposing)
        {
            _usersService.Dispose();
            base.Dispose(disposing);
        }
    }
}
