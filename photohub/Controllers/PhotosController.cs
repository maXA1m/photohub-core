using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.ViewModels;
using PhotoHub.WEB.Mappers;
using PhotoHub.BLL.DTO;

namespace PhotoHub.WEB.Controllers
{
    public class PhotosController : Controller
    {
        private readonly IPhotosService _photosService;
        private readonly IHostingEnvironment _environment;
        private readonly UsersMapper _usersMapper;
        private readonly FiltersMapper _filtersMapper;
        private readonly PhotosMapper _photosMapper;

        public PhotosController(IPhotosService photosService, IHostingEnvironment environment)
        {
            _photosService = photosService;
            _environment = environment;
            _usersMapper = new UsersMapper();
            _filtersMapper = new FiltersMapper();
            _photosMapper = new PhotosMapper();
        }

        [HttpGet, Route("photos")]
        public ViewResult Index()
        {
            return View();
        }
        
        [HttpGet, Route("photos/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            PhotoViewModel photo = _photosMapper.Map(await _photosService.GetAsync(id));

            if (User.Identity.IsAuthenticated)
                ViewBag.CurrentUser = _usersMapper.Map(_photosService.CurrentUserDTO);

            return View(photo);
        }
        
        [Authorize, HttpGet, Route("photos/create")]
        public ActionResult Create()
        {
            ViewBag.Filters = _filtersMapper.MapRange(_photosService.Filters);

            return View(_usersMapper.Map(_photosService.CurrentUserDTO));
        }
        
        [Authorize, HttpPost, ValidateAntiForgeryToken, Route("photos/create")]
        public async Task<ActionResult> Create([Bind("Description, Filter")] PhotoViewModel photo, IFormFile file)
        {
            if (ModelState.IsValid && file.Length > 0)
            {
                string fileName = Convert.ToString(Guid.NewGuid()) + Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));

                photo.Path = fileName;

                fileName = Path.Combine(_environment.WebRootPath, "data/photos") + $@"/{User.Identity.Name}/{fileName}";

                string dir = Path.Combine(_environment.WebRootPath, "data/photos") + $@"/{User.Identity.Name}";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                using (FileStream fs = System.IO.File.Create(fileName))
                {
                    await file.CopyToAsync(fs);
                    await fs.FlushAsync();
                }

                string manufacturer = null;
                string model = null;
                string iso = null;
                string exposure = null;
                string aperture = null;

                int pid = await _photosService.CreateAsync(photo.Filter, photo.Description, photo.Path, manufacturer, model, iso, exposure, aperture);

                return RedirectToAction("Details", "Photos", new { id = pid });
            }
            
            return RedirectToAction("Index");
        }
        
        [Authorize, HttpGet, Route("photos/edit/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            UserDTO user = _photosService.CurrentUserDTO;

            PhotoDTO photo = await _photosService.GetAsync(id);

            if(photo != null && user != null && user.UserName == photo.Owner.UserName)
            {
                ViewBag.LikesCount = photo.Likes.Count();
                ViewBag.Filters = _filtersMapper.MapRange(_photosService.Filters);

                return View(_photosMapper.Map(photo));
            }

            return RedirectToAction("Details", "Photos", new { id = photo.Id });
        }
        
        [Authorize, HttpPost, ValidateAntiForgeryToken, Route("photos/edit/{id}")]
        public async Task<ActionResult> Edit([Bind("Id,Filter,Description")] PhotoViewModel photo)
        {
            if (ModelState.IsValid)
                await _photosService.EditAsync(photo.Id, photo.Filter, photo.Description);

            return RedirectToAction("Details", "Photos", new { id = photo.Id });
        }
        
        [Authorize, HttpPost, Route("photos/delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            UserViewModel user = _usersMapper.Map(_photosService.CurrentUserDTO);

            PhotoDTO photo = await _photosService.GetAsync(id);

            if (photo != null && user.UserName == photo.Owner.UserName)
            {
                var filePath = Path.Combine(_environment.WebRootPath, "data/photos") + $@"/{user.UserName}/{photo.Path}";
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                await _photosService.DeleteAsync(id);
            }

            return RedirectToAction("Details", "Users", new { userName = user.UserName });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _photosService.Dispose();

            base.Dispose(disposing);
        }
    }
}
