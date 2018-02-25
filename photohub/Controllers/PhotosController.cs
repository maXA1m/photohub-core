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

        public PhotosController(IPhotosService photosService, IHostingEnvironment environment)
        {
            _photosService = photosService;
            _environment = environment;
        }

        [HttpGet, Route("Photos/Index")]
        public ViewResult Index()
        {
            return View();
        }
        
        [HttpGet, Route("Photos/Details/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            PhotoViewModel photo = PhotoDTOMapper.ToPhotoViewModel(await _photosService.GetAsync(id));

            if (User.Identity.IsAuthenticated)
                ViewBag.CurrentUser = UserDTOMapper.ToUserViewModel(_photosService.CurrentUserDTO);

            return View(photo);
        }
        
        [Authorize, HttpGet, Route("Photos/Create")]
        public ActionResult Create()
        {
            ViewBag.Filters = FilterDTOMapper.ToFilterViewModels(_photosService.Filters);

            return View(UserDTOMapper.ToUserViewModel(_photosService.CurrentUserDTO));
        }
        
        [Authorize, HttpPost, ValidateAntiForgeryToken, Route("Photos/Create")]
        public async Task<ActionResult> Create([Bind("Description, Filter")] PhotoViewModel photo, IFormFile file)
        {
            if (ModelState.IsValid && file.Length > 0)
            {
                string fileName = Convert.ToString(Guid.NewGuid()) + Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));

                photo.Path = fileName;

                fileName = Path.Combine(_environment.WebRootPath, "data/photos") + $@"/{User.Identity.Name}/{fileName}";

                using (FileStream fs = System.IO.File.Create(fileName))
                {
                    await file.CopyToAsync(fs);
                    await fs.FlushAsync();
                }

                int pid = await _photosService.CreateAsync(photo.Filter, photo.Description, photo.Path);

                return RedirectToAction("Details", "Photos", new { id = pid });
            }
            
            return RedirectToAction("Index");
        }
        
        [Authorize, HttpGet, Route("Photos/Edit/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            UserDTO user = _photosService.CurrentUserDTO;

            PhotoDTO photo = await _photosService.GetAsync(id);

            if(photo != null && user != null && user.UserName == photo.Owner.UserName)
            {
                ViewBag.LikesCount = photo.Likes.Count;
                ViewBag.Filters = FilterDTOMapper.ToFilterViewModels(_photosService.Filters);

                return View(PhotoDTOMapper.ToPhotoViewModel(photo));
            }

            return RedirectToAction("Details", "Photos", new { id = photo.Id });
        }
        
        [Authorize, HttpPost, ValidateAntiForgeryToken, Route("Photos/Edit/{id}")]
        public async Task<ActionResult> Edit([Bind("Id,Filter,Description")] PhotoViewModel photo)
        {
            if (ModelState.IsValid)
                await _photosService.EditAsync(photo.Id, photo.Filter, photo.Description);

            return RedirectToAction("Details", "Photos", new { id = photo.Id });
        }
        
        [Authorize, HttpPost, Route("Photos/Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            UserViewModel user = UserDTOMapper.ToUserViewModel(_photosService.CurrentUserDTO);

            PhotoDTO photo = await _photosService.GetAsync(id);

            if (photo != null && user.UserName == photo.Owner.UserName)
            {
                var filePath = Path.Combine(_environment.WebRootPath, "data/photos") + $@"/{user.UserName}/{photo.Path}";
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                await _photosService.DeleteAsync(id);
            }

            return RedirectToAction("Details", "Users", new { id = user.UserName });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _photosService.Dispose();

            base.Dispose(disposing);
        }
    }
}
