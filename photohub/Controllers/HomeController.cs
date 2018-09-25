using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using PhotoHub.WEB.ViewModels;
using PhotoHub.WEB.Mappers;
using PhotoHub.BLL.Interfaces;

namespace PhotoHub.WEB.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly IPhotosService _photosService;
        private bool _disposed;

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
            ViewBag.Tags = TagsMapper.MapRange(_photosService.Tags);

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
            if (!_disposed)
            {
                if (disposing)
                {
                    _photosService.Dispose();
                }

                base.Dispose(disposing);
            }
        }

        #endregion
    }
}
