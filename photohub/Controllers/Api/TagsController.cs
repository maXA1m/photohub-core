using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.ViewModels;
using PhotoHub.WEB.Mappers;

namespace PhotoHub.WEB.Controllers.Api
{
    [Route("api/tags")]
    public class TagsController : Controller
    {
        #region Fields

        private readonly ITagsService _tagsService;

        private bool _disposed;

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
            return TagsMapper.MapRange(_tagsService.GetAll());
        }

        [HttpGet, Route("{name}")]
        public TagViewModel Get(string name)
        {
            return TagsMapper.Map(_tagsService.Get(name));
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
                }

                base.Dispose(disposing);
            }
        }

        #endregion
    }
}