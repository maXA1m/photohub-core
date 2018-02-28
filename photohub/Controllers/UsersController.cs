using Microsoft.AspNetCore.Mvc;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.Mappers;
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        
        [HttpGet, Route("users/{userName}")]
        public ActionResult Details(string userName)
        {
            UserViewModel user = UserDTOMapper.ToUserDetailsViewModel(_usersService.Get(userName));

            if (User.Identity.IsAuthenticated)
                ViewBag.CurrentUser = UserDTOMapper.ToUserViewModel(_usersService.CurrentUserDTO);

            return View(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _usersService.Dispose();

            base.Dispose(disposing);
        }
    }
}