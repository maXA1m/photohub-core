using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.Extensions;

namespace PhotoHub.WEB.Controllers
{
    public class TagsController : Controller
    {
        #region Fields

        private readonly ITagsService _tagsService;
        private readonly ICurrentUserService _currentUserService;

        private bool _isDisposed;

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
            var item = _tagsService.Get(name).ToViewModel();

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUser = _currentUserService.CurrentUserDTO.ToViewModel();
            }

            return View(item);
        }

        #endregion

        #region Disposing

        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _tagsService.Dispose();
                    _currentUserService.Dispose();
                }

                _isDisposed = true;

                base.Dispose(disposing);
            }
        }

        #endregion
    }
}