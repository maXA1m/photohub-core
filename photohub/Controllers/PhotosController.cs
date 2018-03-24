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
#region Include CoreCompat.System.Drawing
/*
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
*/
#endregion
#region Include Magick.NET
using ImageMagick;
#endregion

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
                #region Saving image
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
                #endregion

                string manufacturer = null;
                string model = null;
                string iso = null;
                string exposure = null;
                string aperture = null;
                string focalLength = null;

                #region CoreCompat.System.Drawing
                /*
                try
                {
                    FileInfo info = new FileInfo(fileName);

                    using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        Image image = Image.FromStream(stream, false, false);

                        PropertyItem item;

                        // Camera brand.
                        item = image.GetPropertyItem(0x010F);
                        manufacturer = Encoding.UTF8.GetString(item.Value, 0, item.Value.Length - 1);

                        // Camera model.
                        item = image.GetPropertyItem(0x0110);
                        model = Encoding.UTF8.GetString(item.Value, 0, item.Value.Length - 1);

                        // Photo iso.
                        item = image.GetPropertyItem(0x8827);
                        iso = Convert.ToInt16(item.Value[1]).ToString();

                        // Photo exposure.
                        item = image.GetPropertyItem(0x829A);
                        exposure = Convert.ToInt64(item.Value[1]).ToString();

                        // Photo aperture.
                        item = image.GetPropertyItem(0x9202);
                        exposure = Convert.ToInt64(item.Value[0]).ToString();

                        // Photo focal length.
                        item = image.GetPropertyItem(0x920A);
                        exposure = Convert.ToInt64(item.Value[1]).ToString();
                    }
                }
                catch (Exception ex)
                {

                }
                */
                #endregion

                #region Magick.NET
                // Read image from file
                using (MagickImage image = new MagickImage(fileName))
                {
                    // Retrieve the exif information
                    ExifProfile profile = image.GetExifProfile();

                    // Check if image contains an exif profile
                    if (profile != null)
                    {
                        // Write all values to the console
                        foreach (ExifValue value in profile.Values)
                        {
                            if (value.Tag == ExifTag.Make)
                                manufacturer = value.ToString();

                            else if (value.Tag == ExifTag.Model)
                                model = value.ToString();

                            else if (value.Tag == ExifTag.ISOSpeed)
                                iso = value.ToString();

                            else if (value.Tag == ExifTag.ExposureTime)
                                exposure = value.ToString();

                            else if (value.Tag == ExifTag.ApertureValue)
                                aperture = value.ToString();

                            else if (value.Tag == ExifTag.FocalLength)
                                focalLength = value.ToString();
                        }
                    }
                }
                #endregion

                int pid = await _photosService.CreateAsync(photo.Filter, photo.Description, photo.Path, manufacturer, model, iso, exposure, aperture, focalLength);

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
