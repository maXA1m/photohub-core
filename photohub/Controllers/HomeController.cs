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

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cover()
        {
            if (User.Identity.IsAuthenticated)
                return Redirect("/Home/Index");

            return View();
        }

        [Authorize]
        public IActionResult Search()
        {
            return View();
        }

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
