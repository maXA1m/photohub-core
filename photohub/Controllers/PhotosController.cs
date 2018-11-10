using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.ViewModels;
using PhotoHub.WEB.Extensions;
using ImageMagick;

namespace PhotoHub.WEB.Controllers
{
    public class PhotosController : Controller
    {
        #region Fields

        private readonly IPhotosService _photosService;
        private readonly IUsersService _usersService;
        private readonly IHostingEnvironment _environment;
        private readonly ICurrentUserService _currentUserService;

        private bool _isDisposed;

        #endregion

        #region .ctors

        public PhotosController(IPhotosService photosService, ICurrentUserService currentUserService, IUsersService usersService, IHostingEnvironment environment)
        {
            _photosService = photosService;
            _environment = environment;
            _currentUserService = currentUserService;
            _usersService = usersService;
        }

        #endregion

        #region Logic

        [HttpGet, Route("photos")]
        public ViewResult Index()
        {
            return View();
        }
        
        [HttpGet, Route("photos/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            var item = await _photosService.GetAsync(id);

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUser = _currentUserService.CurrentUserDTO;
            }

            return View(item.ToViewModel());
        }
        
        [Authorize, HttpGet, Route("photos/create")]
        public ActionResult Create()
        {
            ViewBag.Filters = _photosService.Filters.ToViewModels();

            return View(_currentUserService.CurrentUserDTO.ToViewModel());
        }
        
        [Authorize, HttpPost, ValidateAntiForgeryToken, Route("photos/create")]
        public async Task<ActionResult> Create([Bind("Description, Filter")] PhotoViewModel item, string tags, IFormFile file, int? Iso, double? Aperture, double? Exposure, double? FocalLength, string Model, string Brand)
        {
            if (ModelState.IsValid && file.Length > 0)
            {
                var fileName = Convert.ToString(Guid.NewGuid()) + Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));

                item.Path = fileName;

                fileName = Path.Combine(_environment.WebRootPath, "data/photos") + $@"/{User.Identity.Name}/{fileName}";

                var dir = Path.Combine(_environment.WebRootPath, "data/photos") + $@"/{User.Identity.Name}";

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                using (var fs = System.IO.File.Create(fileName))
                {
                    await file.CopyToAsync(fs);
                    await fs.FlushAsync();
                }

                var manufacturer = Brand;
                var model = Model;

                if(manufacturer == null || model == null)
                {
                    using (var image = new MagickImage(fileName))
                    {
                        var profile = image.GetExifProfile();
                        
                        if (profile != null)
                        {
                            foreach (var value in profile.Values)
                            {
                                if (value.Tag == ExifTag.Make && manufacturer == null)
                                {
                                    manufacturer = value.ToString();
                                }
                                else if (value.Tag == ExifTag.Model && model == null)
                                {
                                    model = value.ToString();
                                }
                            }
                        }
                    }
                }

                int pid = await _photosService.CreateAsync(item.Filter, item.Description, item.Path, manufacturer, model, Iso, Exposure, Aperture, FocalLength, tags);

                return RedirectToAction("Details", "Photos", new { id = pid });
            }
            
            return RedirectToAction("Index");
        }
        
        [Authorize, HttpGet, Route("photos/edit/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            var item = await _photosService.GetAsync(id);
            var user = _usersService.Get(item.Owner.UserName).ToViewModel();

            if (item != null && user != null && (user.UserName == item.Owner.UserName || User.IsInRole("Admin")))
            {
                ViewBag.LikesCount = item.Likes.Count();
                ViewBag.Filters = _photosService.Filters.ToViewModels();

                return View(item.ToViewModel());
            }

            return RedirectToAction("Details", "Photos", new { id = item.Id });
        }
        
        [Authorize, HttpPost, ValidateAntiForgeryToken, Route("photos/edit/{id}")]
        public async Task<ActionResult> Edit([Bind("Id, Filter, Description")] PhotoViewModel item, string tags, string model, string brand, int? iso, double? aperture, double? exposure, double? focalLength)
        {
            if (ModelState.IsValid)
            {
                await _photosService.EditAsync(item.Id, item.Filter, item.Description, tags, model, brand, iso, aperture, exposure, focalLength);
            }

            return RedirectToAction("Details", "Photos", new { id = item.Id });
        }
        
        [Authorize, HttpPost, Route("photos/delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var item = await _photosService.GetAsync(id);
            var user = _usersService.Get(item.Owner.UserName).ToViewModel();

            if (item != null && (user.UserName == item.Owner.UserName || User.IsInRole("Admin")))
            {
                var filePath = Path.Combine(_environment.WebRootPath, "data/photos") + $@"/{user.UserName}/{item.Path}";

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                await _photosService.DeleteAsync(id);
            }

            return RedirectToAction("Details", "Users", new { userName = user.UserName });
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
