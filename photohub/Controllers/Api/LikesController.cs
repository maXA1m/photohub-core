using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PhotoHub.BLL.Interfaces;

namespace PhotoHub.WEB.Controllers.Api
{
    [Route("api/likes")]
    public class LikesController : Controller
    {
        private readonly ILikesService _likesService;

        public LikesController(ILikesService likesService)
        {
            _likesService = likesService;
        }

        [Authorize, HttpPost, Route("add/{photoId}")]
        public async Task Add(int photoId)
        {
            await _likesService.AddAsync(photoId);
        }

        [Authorize, HttpPost, Route("delete/{photoId}")]
        public async Task Delete(int photoId)
        {
            await _likesService.DeleteAsync(photoId);
        }

        protected override void Dispose(bool disposing)
        {
            _likesService.Dispose();
            base.Dispose(disposing);
        }
    }
}