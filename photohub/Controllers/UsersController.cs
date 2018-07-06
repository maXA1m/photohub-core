using Microsoft.AspNetCore.Mvc;
using PhotoHub.BLL.Interfaces;
#region using PhotoHub.WEB
using PhotoHub.WEB.Mappers;
using PhotoHub.WEB.ViewModels;
#endregion

namespace PhotoHub.WEB.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly ICurrentUserService _currentUserService;
        #region private readonly mappers
        private readonly UsersDetailsMapper _usersDetailsMapper;
        private readonly UsersMapper _usersMapper;
        #endregion

        public UsersController(IUsersService usersService, ICurrentUserService currentUserService)
        {
            _usersService = usersService;
            _currentUserService = currentUserService;
            _usersDetailsMapper = new UsersDetailsMapper();
            _usersMapper = new UsersMapper();
        }
        
        [HttpGet, Route("users/{userName}")]
        public ActionResult Details(string userName)
        {
            UserViewModel item = _usersDetailsMapper.Map(_usersService.Get(userName));

            if (User.Identity.IsAuthenticated)
                ViewBag.CurrentUser = _usersMapper.Map(_currentUserService.GetDTO);

            return View(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _usersService.Dispose();

            base.Dispose(disposing);
        }
    }
}