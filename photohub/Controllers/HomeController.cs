using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PhotoHub.WEB.ViewModels;
using Microsoft.AspNetCore.Http;

namespace PhotoHub.WEB.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        [Authorize, Route("")]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return View();

            return View("Cover");
        }

        [Authorize, Route("Search")]
        public IActionResult Search()
        {
            return View();
        }

        [Route("About")]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
