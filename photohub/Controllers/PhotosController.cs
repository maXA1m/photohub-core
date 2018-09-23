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
using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;
using PhotoHub.WEB.Mappers;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.Text;
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
            PhotoViewModel item = PhotosMapper.Map(await _photosService.GetAsync(id));

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUser = UsersMapper.Map(_currentUserService.GetDTO);
            }

            return View(item);
        }
        
        [Authorize, HttpGet, Route("photos/create")]
        public ActionResult Create()
        {
            ViewBag.Filters = FiltersMapper.MapRange(_photosService.Filters);
            //ViewBag.Tags = _tagsMapper.MapRange(_photosService.Tags);

            return View(UsersMapper.Map(_currentUserService.GetDTO));
        }
        
        [Authorize, HttpPost, ValidateAntiForgeryToken, Route("photos/create")]
        public async Task<ActionResult> Create([Bind("Description, Filter")] PhotoViewModel item, string tags, IFormFile file, int? Iso, double? Aperture, double? Exposure, double? FocalLength, string Model, string Brand)
        {
            if (ModelState.IsValid && file.Length > 0)
            {
                string fileName = Convert.ToString(Guid.NewGuid()) + Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));

                item.Path = fileName;

                fileName = Path.Combine(_environment.WebRootPath, "data/photos") + $@"/{User.Identity.Name}/{fileName}";

                string dir = Path.Combine(_environment.WebRootPath, "data/photos") + $@"/{User.Identity.Name}";

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                using (FileStream fs = System.IO.File.Create(fileName))
                {
                    await file.CopyToAsync(fs);
                    await fs.FlushAsync();
                }

                string manufacturer = Brand;
                string model = Model;

                if(manufacturer == null || model == null)
                {
                    // Read image from file
                    using (var image = new MagickImage(fileName))
                    {
                        // Retrieve the exif information
                        ExifProfile profile = image.GetExifProfile();

                        // Check if image contains an exif profile
                        if (profile != null)
                        {
                            // Write all values to the console
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
            PhotoDTO item = await _photosService.GetAsync(id);
            UserViewModel user = UsersMapper.Map(_usersService.Get(item.Owner.UserName));

            if (item != null && user != null && (user.UserName == item.Owner.UserName || User.IsInRole("Admin")))
            {
                ViewBag.LikesCount = item.Likes.Count();
                ViewBag.Filters = FiltersMapper.MapRange(_photosService.Filters);
                //ViewBag.Tags = _tagsMapper.MapRange(_photosService.Tags);

                return View(PhotosMapper.Map(item));
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
            PhotoDTO item = await _photosService.GetAsync(id);
            UserViewModel user = UsersMapper.Map(_usersService.Get(item.Owner.UserName));

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

        #region Logic

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _photosService.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion
    }
}
