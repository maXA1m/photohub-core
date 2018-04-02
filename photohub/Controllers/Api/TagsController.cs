#region using System/Microsoft
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
#endregion
#region using PhotoHub.BLL
using PhotoHub.BLL.Interfaces;
using PhotoHub.BLL.DTO;
#endregion
#region using PhotoHub.WEB
using PhotoHub.WEB.ViewModels;
using PhotoHub.WEB.Mappers;
#endregion

namespace PhotoHub.WEB.Controllers.Api
{
    [Route("api/tags")]
    public class TagsController : Controller
    {
        private readonly ITagsService _tagsService;
        private readonly IMapper<TagViewModel, TagDTO> _tagsMapper;

        public TagsController(ITagsService tagsService)
        {
            _tagsService = tagsService;
            _tagsMapper = new TagsMapper();
        }

        [HttpGet, Route("")]
        public IEnumerable<TagViewModel> GetAll()
        {
            return _tagsMapper.MapRange(_tagsService.GetAll());
        }

        [HttpGet, Route("{name}")]
        public TagViewModel Get(string name)
        {
            return _tagsMapper.Map(_tagsService.Get(name));
        }

        protected override void Dispose(bool disposing)
        {
            _tagsService.Dispose();
            base.Dispose(disposing);
        }
    }
}