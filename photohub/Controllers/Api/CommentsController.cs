using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Controllers.Api
{
    [Route("api/comments")]
    public class CommentsController : Controller
    {
        private readonly ICommentsService _commentsService;



        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [Authorize, HttpPost, Route("add")]
        public async Task<int?> Add(int photoId, string text)
        {
            return await _commentsService.AddAsync(photoId, text);
        }
        
        [Authorize, HttpPost, Route("delete/{id}")]
        public async Task Delete(int id)
        {
            await _commentsService.DeleteAsync(id);
        }

        protected override void Dispose(bool disposing)
        {
            _commentsService.Dispose();
            base.Dispose(disposing);
        }
    }
}