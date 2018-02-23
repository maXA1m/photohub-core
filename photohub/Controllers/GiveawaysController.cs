using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.ViewModels;
using PhotoHub.WEB.Mappers;
using PhotoHub.DAL.Entities;

namespace PhotoHub.WEB.Controllers
{
    public class GiveawaysController : Controller
    {
        private readonly IGiveawaysService _giveawaysService;
        private readonly IHostingEnvironment _environment;

        public GiveawaysController(IGiveawaysService giveawaysService, IHostingEnvironment environment)
        {
            _giveawaysService = giveawaysService;
            _environment = environment;
        }

        [HttpGet, Route("Giveaways")]
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet, Route("Giveaways/Details/{id}")]
        public ActionResult Details(int id)
        {
            ViewBag.CurrentUser = UserDTOMapper.ToUserViewModel(_giveawaysService.CurrentUserDTO);  

            return View(_giveawaysService.Get(id));
        }
        
        [Authorize, HttpGet, Route("Giveaways/Create")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken, Route("Giveaways/Create")]
        public async Task<ActionResult> Create([Bind("Name,Email,About")] GiveawayViewModel giveaway, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                IFormFile file = files[0];
                if (file.Length > 0)
                {
                    string fileName = Convert.ToString(Guid.NewGuid()) + Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));

                    giveaway.Avatar = fileName;

                    fileName = Path.Combine(_environment.WebRootPath, "data/giveaways") + $@"/{giveaway.Name}/{fileName}";

                    using (FileStream fs = System.IO.File.Create(fileName))
                    {
                        await file.CopyToAsync(fs);
                        await fs.FlushAsync();
                    }
                }

                int gid = await _giveawaysService.CreateAsync(giveaway.Name, giveaway.Email, giveaway.About, giveaway.Avatar);

                return RedirectToAction("Details", new { id = gid });
            }
            return RedirectToAction("Index");
        }
        
        [Authorize, HttpGet, Route("Giveaways/Edit/{id}")]
        public async Task<ActionResult> Edit(int id)
        {
            return View(GiveawaysDTOMapper.ToGiveawayDetailsViewModel(await _giveawaysService.GetAsync(id)));
        }

        [Authorize, HttpPost, ValidateAntiForgeryToken, Route("Giveaways/Edit/{id}")]
        public async Task<ActionResult> Edit([Bind("Id,Name,Email,About")] GiveawayViewModel giveaway, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                IFormFile file = files[0];
                if (file.Length > 0)
                {
                    string fileName = Convert.ToString(Guid.NewGuid()) + Path.GetExtension(ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"'));

                    giveaway.Avatar = fileName;

                    fileName = Path.Combine(_environment.WebRootPath, "data/giveaways") + $@"/{giveaway.Name}/{fileName}";

                    using (FileStream fs = System.IO.File.Create(fileName))
                    {
                        await file.CopyToAsync(fs);
                        await fs.FlushAsync();
                    }
                }

                await _giveawaysService.EditAsync(giveaway.Id, giveaway.Name, giveaway.Email, giveaway.Avatar, giveaway.About);

                return View(giveaway);
            }
            return View(giveaway);
        }
        
        [Authorize, HttpPost, ValidateAntiForgeryToken, Route("Giveaways/Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _giveawaysService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
        
        [Authorize, HttpPost, Route("Giveaways/Enter")]
        public async Task<ActionResult> Enter(int id)
        {
            await _giveawaysService.EnterAsync(id);

            return RedirectToAction("Details", "Giveaways", new { id });
        }
        
        [Authorize, HttpPost, Route("Giveaways/Leave")]
        public async Task<ActionResult> Leave(int id)
        {
            await _giveawaysService.LeaveAsync(id);

            return RedirectToAction("Details", "Giveaways", new { id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _giveawaysService.Dispose();

            base.Dispose(disposing);
        }
    }
}
