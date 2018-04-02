#region using System/Microsoft
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
#endregion
#region using PhotoHub.DAL
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Entities;
#endregion
#region using PhotoHub.BLL
using PhotoHub.BLL.Interfaces;
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Mappers;
using System.Collections.Generic;
#endregion

namespace PhotoHub.BLL.Services
{
    public class TagsService : ITagsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #region private readonly mappers
        private readonly UsersMapper _usersMapper;
        private readonly TagsMapper _tagsMapper;
        #endregion

        public ApplicationUser CurrentUser => _unitOfWork.Users.Find(u => u.UserName == _httpContextAccessor.HttpContext.User.Identity.Name).FirstOrDefault();
        public UserDTO CurrentUserDTO
        {
            get
            {
                ApplicationUser user = CurrentUser;

                return _usersMapper.Map(
                    user,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == user.Id).FirstOrDefault() != null
                );
            }
        }

        public TagsService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _usersMapper = new UsersMapper();
            _tagsMapper = new TagsMapper();
        }

        public IEnumerable<TagDTO> GetAll()
        {
            return _tagsMapper.MapRange(_unitOfWork.Tags.GetAll());
        }

        public TagDTO Get(string name)
        {
            Tag tag = _unitOfWork.Tags.Find(t => t.Name == name).FirstOrDefault();
            if (tag != null)
                return _tagsMapper.Map(tag);

            return null;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}