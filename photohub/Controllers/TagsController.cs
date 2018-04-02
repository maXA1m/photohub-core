#region using System/Microsoft
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
#endregion
#region using PhotoHub.BLL
using PhotoHub.BLL.Interfaces;
using PhotoHub.BLL.DTO;
#endregion
#region using PhotoHub.WEB
using PhotoHub.WEB.ViewModels;
using PhotoHub.WEB.Mappers;
#endregion

namespace PhotoHub.WEB.Controllers
{
    public class TagsController : Controller
    {
        private readonly ITagsService _tagsService;
        #region private readonly mappers
        private readonly UsersMapper _usersMapper;
        private readonly TagsMapper _tagsMapper;
        #endregion

        public TagsController(ITagsService tagsService)
        {
            _tagsService = tagsService;
            _usersMapper = new UsersMapper();
            _tagsMapper = new TagsMapper();
        }

        [Authorize, HttpGet, Route("tag/{name}")]
        public ActionResult Details(string name)
        {
            TagViewModel item = _tagsMapper.Map(_tagsService.Get(name));

            if (User.Identity.IsAuthenticated)
                ViewBag.CurrentUser = _usersMapper.Map(_tagsService.CurrentUserDTO);

            return View(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _tagsService.Dispose();

            base.Dispose(disposing);
        }
    }
}