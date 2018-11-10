using Microsoft.AspNetCore.Mvc;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.Extensions;

namespace PhotoHub.WEB.Controllers
{
    public class UsersController : Controller
    {
        #region Fields

        private readonly IUsersService _usersService;
        private readonly ICurrentUserService _currentUserService;

        private bool _isDisposed;

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
            var item = _usersService.Get(userName).ToViewModel();

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUser = _currentUserService.CurrentUserDTO.ToViewModel();
            }

            return View(item);
        }

        #endregion

        #region Disposing

        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _usersService.Dispose();
                    _currentUserService.Dispose();
                }

                _isDisposed = true;

                base.Dispose(disposing);
            }
        }

        #endregion
    }
}