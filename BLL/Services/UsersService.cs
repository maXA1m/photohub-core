using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Entities;
using PhotoHub.BLL.Interfaces;
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Mappers;

namespace PhotoHub.BLL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationUser CurrentUser => _unitOfWork.Users.Find(u => u.UserName == _httpContextAccessor.HttpContext.User.Identity.Name).FirstOrDefault();
        public UserDTO CurrentUserDTO {
            get
            {
                ApplicationUser user = CurrentUser;

                return UserMapper.ToUserDTO(
                    user,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == user.Id).FirstOrDefault() != null
                );
            }
        }

        public UsersService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<UserDTO> GetAll(int page, int pageSize)
        {
            ApplicationUser currentUser = CurrentUser;
            IEnumerable<ApplicationUser> users = _unitOfWork.Users.GetAll(page, pageSize);
            List<UserDTO> userDTOs = new List<UserDTO>(users.Count());

            foreach (ApplicationUser user in users)
            {
                userDTOs.Add(UserMapper.ToUserDTO(
                    user,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null
                ));
            }

            return userDTOs;
        }
        public async Task<IEnumerable<UserDTO>> GetAllAsync(int page, int pageSize)
        {
            ApplicationUser currentUser = CurrentUser;
            IEnumerable<ApplicationUser> users = await _unitOfWork.Users.GetAllAsync(page, pageSize);
            List<UserDTO> userDTOs = new List<UserDTO>(users.Count());

            foreach (ApplicationUser user in users)
            {
                userDTOs.Add(UserMapper.ToUserDTO(
                    user,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null
                ));
            }

            return userDTOs;
        }

        public UserDetailsDTO Get(string userName)
        {
            ApplicationUser currentUser = CurrentUser;
            ApplicationUser user = _unitOfWork.Users.Find(u => u.UserName == userName).FirstOrDefault();

            List<UserDTO> followings = new List<UserDTO>();
            List<UserDTO> followers = new List<UserDTO>();

            foreach (Following following in _unitOfWork.Followings.Find(f => f.UserId == user.Id))
            {
                followings.Add(UserMapper.ToUserDTO(
                    following.FollowedUser,
                    _unitOfWork.Confirmations.Find(c => c.UserId == following.FollowedUserId).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == following.FollowedUserId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null
                ));
            }

            foreach (Following follower in _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id))
            {
                followers.Add(UserMapper.ToUserDTO(
                    follower.User,
                    _unitOfWork.Confirmations.Find(c => c.UserId == follower.UserId).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == follower.UserId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == follower.UserId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null
                ));
            }

            return UserMapper.ToUserDetailsDTO(
                user,
                _unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null,
                _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == currentUser.Id).FirstOrDefault() != null,
                _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == currentUser.Id).FirstOrDefault() != null,
                _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null,
                followings,
                followers
            );
        }

        public IEnumerable<UserDTO> Search(int page, string search, int pageSize)
        {
            ApplicationUser currentUser = CurrentUser;
            IEnumerable<ApplicationUser> users = _unitOfWork.Users.Find(u => 
                u.UserName != currentUser.UserName && 
                (   
                    String.IsNullOrEmpty(search) || 
                    u.Email.ToLower().Contains(search.ToLower()) ||
                    u.UserName.ToLower().Contains(search.ToLower())
                )
            ).Skip(page * pageSize).Take(pageSize);

            List<UserDTO> userDTOs = new List<UserDTO>(users.Count());

            foreach(ApplicationUser user in users)
            {
                userDTOs.Add(UserMapper.ToUserDTO(
                    user,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null
                ));
            }

            return userDTOs;
        }

        public void Follow(string follow)
        {
            ApplicationUser currentUser = CurrentUser;
            ApplicationUser followedUser = _unitOfWork.Users.Find(u => u.UserName == follow).FirstOrDefault();

            if (currentUser != null && followedUser != null && currentUser.UserName != follow && _unitOfWork.Followings.Find(f => f.UserId == currentUser.Id && f.FollowedUserId == followedUser.Id).FirstOrDefault() == null)
            {
                _unitOfWork.Followings.Create(new Following()
                {
                    UserId = currentUser.Id,
                    FollowedUserId = followedUser.Id
                });

                _unitOfWork.Save();
            }
        }
        public async Task FollowAsync(string follow)
        {
            ApplicationUser currentUser = CurrentUser;
            ApplicationUser followedUser = _unitOfWork.Users.Find(u => u.UserName == follow).FirstOrDefault();

            if (currentUser != null && followedUser != null && currentUser.UserName != follow && _unitOfWork.Followings.Find(f => f.UserId == currentUser.Id && f.FollowedUserId == followedUser.Id).FirstOrDefault() == null)
            {
                await _unitOfWork.Followings.CreateAsync(new Following()
                {
                    UserId = currentUser.Id,
                    FollowedUserId = followedUser.Id
                });

                await _unitOfWork.SaveAsync();
            }
        }

        public void DismissFollow(string follow)
        {
            ApplicationUser currentUser = CurrentUser;
            ApplicationUser followedUser = _unitOfWork.Users.Find(u => u.UserName == follow).FirstOrDefault();
            Following following = _unitOfWork.Followings.Find(f => f.UserId == currentUser.Id && f.FollowedUserId == followedUser.Id).FirstOrDefault();

            if (currentUser != null && followedUser != null && currentUser.UserName != follow && following != null)
            {
                _unitOfWork.Followings.Delete(following.Id);

                _unitOfWork.Save();
            }
        }
        public async Task DismissFollowAsync(string follow)
        {
            ApplicationUser currentUser = CurrentUser;
            ApplicationUser followedUser = _unitOfWork.Users.Find(u => u.UserName == follow).FirstOrDefault();
            Following following = _unitOfWork.Followings.Find(f => f.UserId == currentUser.Id && f.FollowedUserId == followedUser.Id).FirstOrDefault();

            if (currentUser != null && followedUser != null && currentUser.UserName != follow && following != null)
            {
                await _unitOfWork.Followings.DeleteAsync(following.Id);

                await _unitOfWork.SaveAsync();
            }
        }

        public void Block(string block)
        {
            ApplicationUser currentUser = CurrentUser;
            ApplicationUser blockedUser = _unitOfWork.Users.Find(u => u.UserName == block).FirstOrDefault();

            if (currentUser != null && blockedUser != null && currentUser.UserName != block && _unitOfWork.Blockings.Find(b => b.UserId == currentUser.Id && b.BlockedUserId == currentUser.Id).FirstOrDefault() == null)
            {
                _unitOfWork.Blockings.Create(new BlackList()
                {
                    UserId = currentUser.Id,
                    BlockedUserId = blockedUser.Id
                });

                _unitOfWork.Save();
            }
        }
        public async Task BlockAsync(string block)
        {
            ApplicationUser currentUser = CurrentUser;
            ApplicationUser blockedUser = _unitOfWork.Users.Find(u => u.UserName == block).FirstOrDefault();

            if (currentUser != null && blockedUser != null && currentUser.UserName != block && _unitOfWork.Blockings.Find(b => b.UserId == currentUser.Id && b.BlockedUserId == currentUser.Id).FirstOrDefault() == null)
            {
                await _unitOfWork.Blockings.CreateAsync(new BlackList()
                {
                    UserId = currentUser.Id,
                    BlockedUserId = blockedUser.Id
                });

                await _unitOfWork.SaveAsync();
            }
        }

        public void DismissBlock(string block)
        {
            ApplicationUser currentUser = CurrentUser;
            ApplicationUser blockedUser = _unitOfWork.Users.Find(u => u.UserName == block).FirstOrDefault();
            BlackList blocking = _unitOfWork.Blockings.Find(b => b.UserId == currentUser.Id && b.BlockedUserId == blockedUser.Id).FirstOrDefault();

            if (currentUser != null && blockedUser != null && currentUser.UserName != block && blocking != null)
            {
                _unitOfWork.Blockings.Delete(blocking.Id);
                _unitOfWork.Save();
            }
        }
        public async Task DismissBlockAsync(string block)
        {
            ApplicationUser currentUser = CurrentUser;
            ApplicationUser blockedUser = _unitOfWork.Users.Find(u => u.UserName == block).FirstOrDefault();
            BlackList blocking = _unitOfWork.Blockings.Find(b => b.UserId == currentUser.Id && b.BlockedUserId == blockedUser.Id).FirstOrDefault();

            if (currentUser != null && blockedUser != null && currentUser.UserName != block && blocking != null)
            {
                await _unitOfWork.Blockings.DeleteAsync(blocking.Id);
                await _unitOfWork.SaveAsync();
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}