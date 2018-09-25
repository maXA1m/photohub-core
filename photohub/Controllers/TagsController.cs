using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.ViewModels;
using PhotoHub.WEB.Mappers;

namespace PhotoHub.WEB.Controllers
{
    public class TagsController : Controller
    {
        #region Fields

        private readonly ITagsService _tagsService;
        private readonly ICurrentUserService _currentUserService;

        private bool _disposed;

        #endregion

        #region .ctors

        public TagsController(ITagsService tagsService, ICurrentUserService currentUserService)
        {
            _tagsService = tagsService;
            _currentUserService = currentUserService;
        }

        #endregion

        #region Logic

        [Authorize, HttpGet, Route("tag/{name}")]
        public ActionResult Details(string name)
        {
            TagViewModel item = TagsMapper.Map(_tagsService.Get(name));

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUser = UsersMapper.Map(_currentUserService.GetDTO);
            }

            return View(item);
        }

        #endregion

        #region Disposing

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _tagsService.Dispose();
                    _currentUserService.Dispose();
                }

                _disposed = true;

                base.Dispose(disposing);
            }
        }

        #endregion
    }
}