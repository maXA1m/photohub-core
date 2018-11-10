using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PhotoHub.WEB.ViewModels;
using PhotoHub.WEB.Extensions;
using PhotoHub.BLL.Interfaces;

namespace PhotoHub.WEB.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly IPhotosService _photosService;

        private bool _isDisposed;

        #endregion

        #region .ctors

        public HomeController(IPhotosService photosService)
        {
            _photosService = photosService;
        }

        #endregion

        #region Logic

        [Route("")]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }

            return View("Cover");
        }

        [Authorize, Route("search")]
        public IActionResult Search()
        { 
            ViewBag.Tags = _photosService.Tags.ToViewModels();

            return View();
        }

        [Route("about")]
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        #region Disposing

        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _photosService.Dispose();
                }

                _isDisposed = true;

                base.Dispose(disposing);
            }
        }

        #endregion
    }
}
