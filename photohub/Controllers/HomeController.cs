#region using System/Microsoft
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
#endregion
#region using PhotoHub.WEB
using PhotoHub.WEB.ViewModels;
using PhotoHub.WEB.Mappers;
#endregion
using PhotoHub.BLL.Interfaces;

namespace PhotoHub.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPhotosService _photosService;
        #region private readonly mappers
        private readonly IsosMapper _isosMapper;
        private readonly ExposuresMapper _exposuresMapper;
        private readonly AperturesMapper _aperturesMapper;
        #endregion

        public HomeController(IPhotosService photosService)
        {
            _photosService = photosService;
            _isosMapper = new IsosMapper();
            _exposuresMapper = new ExposuresMapper();
            _aperturesMapper = new AperturesMapper();
        }

        [Route("")]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return View();

            return View("Cover");
        }

        [Authorize, Route("search")]
        public IActionResult Search()
        { 
            ViewBag.Isos = _isosMapper.MapRange(_photosService.Isos);
            ViewBag.Exposures = _exposuresMapper.MapRange(_photosService.Exposures);
            ViewBag.Apertures = _aperturesMapper.MapRange(_photosService.Apertures);

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
    }
}
