using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.ViewModels;
using PhotoHub.WEB.Extensions;

namespace PhotoHub.WEB.Controllers.Api
{
    [Route("api/tags")]
    public class TagsController : Controller
    {
        #region Fields

        private readonly ITagsService _tagsService;

        private bool _isDisposed;

        #endregion

        #region .ctors

        public TagsController(ITagsService tagsService)
        {
            _tagsService = tagsService;
        }

        #endregion

        #region Logic

        [HttpGet, Route("")]
        public IEnumerable<TagViewModel> GetAll()
        {
            return _tagsService.GetAll().ToViewModels();
        }

        [HttpGet, Route("{name}")]
        public TagViewModel Get(string name)
        {
            return _tagsService.Get(name).ToViewModel();
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
                }

                _isDisposed = true;

                base.Dispose(disposing);
            }
        }

        #endregion
    }
}