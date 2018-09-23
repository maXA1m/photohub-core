using Microsoft.AspNetCore.Mvc;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.Mappers;
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Controllers
{
    public class UsersController : Controller
    {
        #region Fields

        private readonly IUsersService _usersService;
        private readonly ICurrentUserService _currentUserService;

        #endregion

        #region .ctors

        public UsersController(IUsersService usersService, ICurrentUserService currentUserService)
        {
            _usersService = usersService;
            _currentUserService = currentUserService;
        }

        #endregion

        #region Logic

        [HttpGet, Route("users/{userName}")]
        public ActionResult Details(string userName)
        {
            UserViewModel item = UsersDetailsMapper.Map(_usersService.Get(userName));

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUser = UsersMapper.Map(_currentUserService.GetDTO);
            }

            return View(item);
        }

        #endregion

        #region Disposing

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _usersService.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}