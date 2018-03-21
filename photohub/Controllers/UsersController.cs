using Microsoft.AspNetCore.Mvc;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.Mappers;
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly UsersDetailsMapper _usersDetailsMapper;
        private readonly UsersMapper _usersMapper;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
            _usersDetailsMapper = new UsersDetailsMapper();
            _usersMapper = new UsersMapper();
        }
        
        [HttpGet, Route("users/{userName}")]
        public ActionResult Details(string userName)
        {
            UserViewModel user = _usersDetailsMapper.Map(_usersService.Get(userName));

            if (User.Identity.IsAuthenticated)
                ViewBag.CurrentUser = _usersMapper.Map(_usersService.CurrentUserDTO);

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