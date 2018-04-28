#region using System/Microsoft
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
#endregion
#region using PhotoHub.DAL
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Entities;
#endregion
#region using PhotoHub.BLL
using PhotoHub.BLL.Interfaces;
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Mappers;
#endregion

namespace PhotoHub.BLL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #region private readonly mappers
        private readonly UsersMapper _usersMapper;
        private readonly UsersDetailsMapper _usersDetailsMapper;
        #endregion

        public User CurrentUser => _unitOfWork.Users.Find(u => u.UserName == _httpContextAccessor.HttpContext.User.Identity.Name).FirstOrDefault();
        public UserDTO CurrentUserDTO {
            get
            {
                User user = CurrentUser;

                return _usersMapper.Map(
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
            _usersMapper = new UsersMapper();
            _usersDetailsMapper = new UsersDetailsMapper();
        }

        public IEnumerable<UserDTO> GetAll(int page, int pageSize)
        {
            User currentUser = CurrentUser;
            IEnumerable<User> users = _unitOfWork.Users.GetAll(page, pageSize);
            List<UserDTO> userDTOs = new List<UserDTO>(users.Count());

            foreach (User user in users)
            {
                userDTOs.Add(_usersMapper.Map(
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
            User currentUser = CurrentUser;
            User user = _unitOfWork.Users.Find(u => u.UserName == userName).FirstOrDefault();

            List<UserDTO> followings = new List<UserDTO>();
            List<UserDTO> followers = new List<UserDTO>();

            if(currentUser == null)
            {
                foreach (Following following in _unitOfWork.Followings.Find(f => f.UserId == user.Id))
                {
                    followings.Add(_usersMapper.Map(
                        following.FollowedUser,
                        _unitOfWork.Confirmations.Find(c => c.UserId == following.FollowedUserId).FirstOrDefault() != null,
                        false, false, false
                    ));
                }

                foreach (Following follower in _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id))
                {
                    followers.Add(_usersMapper.Map(
                        follower.User,
                        _unitOfWork.Confirmations.Find(c => c.UserId == follower.UserId).FirstOrDefault() != null,
                        false, false, false
                    ));
                }

                return _usersDetailsMapper.Map(
                    user,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null,
                    false,
                    false,
                    false,
                    followings,
                    followers,
                    null
                );
            }
            else
            {
                List<UserDTO> mutuals = new List<UserDTO>();

                foreach (Following following in _unitOfWork.Followings.Find(f => f.UserId == user.Id))
                {
                    followings.Add(_usersMapper.Map(
                        following.FollowedUser,
                        _unitOfWork.Confirmations.Find(c => c.UserId == following.FollowedUserId).FirstOrDefault() != null,
                        _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null,
                        _unitOfWork.Followings.Find(f => f.FollowedUserId == following.FollowedUserId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                        _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null
                    ));
                }

                foreach (Following follower in _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id))
                {
                    followers.Add(_usersMapper.Map(
                        follower.User,
                        _unitOfWork.Confirmations.Find(c => c.UserId == follower.UserId).FirstOrDefault() != null,
                        _unitOfWork.Followings.Find(f => f.FollowedUserId == follower.UserId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                        _unitOfWork.Blockings.Find(b => b.BlockedUserId == follower.UserId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                        _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null
                    ));
                }

                if(followers.Count > 0)
                {
                    foreach (Following follower in _unitOfWork.Followings.Find(f => f.FollowedUserId == currentUser.Id))
                    {
                        if (follower.UserId != user.Id && _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == follower.UserId).FirstOrDefault() != null)
                        {
                            mutuals.Add(_usersMapper.Map(
                                follower.User,
                                _unitOfWork.Confirmations.Find(c => c.UserId == follower.UserId).FirstOrDefault() != null,
                                _unitOfWork.Followings.Find(f => f.FollowedUserId == follower.UserId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                                _unitOfWork.Blockings.Find(b => b.BlockedUserId == follower.UserId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                                _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null
                            ));
                        }
                    }
                }

                return _usersDetailsMapper.Map(
                    user,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null,
                    followings,
                    followers,
                    mutuals
                );
            }
        }

        public IEnumerable<UserDTO> GetBlocked(int page, int pageSize)
        {
            User currentUser = CurrentUser;
            IEnumerable<BlackList> blacklists = _unitOfWork.Blockings.Find(b => b.UserId == currentUser.Id);
            List<User> users = new List<User>();

            foreach(var blocking in blacklists)
                users.Add(blocking.BlockedUser);

            List<UserDTO> userDTOs = new List<UserDTO>(pageSize);
            foreach (User user in users)
            {
                userDTOs.Add(_usersMapper.Map(
                    user,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null
                ));
            }

            return userDTOs;
        }

        public IEnumerable<UserDTO> Search(int page, string search, int pageSize)
        {
            User currentUser = CurrentUser;
            IEnumerable<User> users;
            if (String.IsNullOrEmpty(search))
            {
                users = _unitOfWork.Users.Find(u => u.UserName != currentUser.UserName).OrderByDescending(u => u.Date).Skip(page * pageSize).Take(pageSize);
            }
            else
            {
                users = _unitOfWork.Users.Find(u =>
                    u.UserName != currentUser.UserName &&
                    (
                        u.UserName.ToLower().Contains(search.ToLower()) || (!String.IsNullOrEmpty(u.RealName) ? u.RealName.ToLower().Contains(search.ToLower()) : false)
                    )
                ).OrderBy(u => u.Date).Skip(page * pageSize).Take(pageSize);
            }

            List<UserDTO> userDTOs = new List<UserDTO>();

            foreach(User user in users)
            {
                userDTOs.Add(_usersMapper.Map(
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
            User currentUser = CurrentUser;
            User followedUser = _unitOfWork.Users.Find(u => u.UserName == follow).FirstOrDefault();

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
            User currentUser = CurrentUser;
            User followedUser = _unitOfWork.Users.Find(u => u.UserName == follow).FirstOrDefault();

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
            User currentUser = CurrentUser;
            User followedUser = _unitOfWork.Users.Find(u => u.UserName == follow).FirstOrDefault();
            Following following = _unitOfWork.Followings.Find(f => f.UserId == currentUser.Id && f.FollowedUserId == followedUser.Id).FirstOrDefault();

            if (currentUser != null && followedUser != null && currentUser.UserName != follow && following != null)
            {
                _unitOfWork.Followings.Delete(following.Id);

                _unitOfWork.Save();
            }
        }
        public async Task DismissFollowAsync(string follow)
        {
            User currentUser = CurrentUser;
            User followedUser = _unitOfWork.Users.Find(u => u.UserName == follow).FirstOrDefault();
            Following following = _unitOfWork.Followings.Find(f => f.UserId == currentUser.Id && f.FollowedUserId == followedUser.Id).FirstOrDefault();

            if (currentUser != null && followedUser != null && currentUser.UserName != follow && following != null)
            {
                await _unitOfWork.Followings.DeleteAsync(following.Id);

                await _unitOfWork.SaveAsync();
            }
        }

        public void Block(string block)
        {
            User currentUser = CurrentUser;
            User blockedUser = _unitOfWork.Users.Find(u => u.UserName == block).FirstOrDefault();

            if (currentUser != null && blockedUser != null && currentUser.UserName != block && _unitOfWork.Blockings.Find(b => b.UserId == currentUser.Id && b.BlockedUserId == blockedUser.Id).FirstOrDefault() == null)
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
            User currentUser = CurrentUser;
            User blockedUser = _unitOfWork.Users.Find(u => u.UserName == block).FirstOrDefault();

            if (currentUser != null && blockedUser != null && currentUser.UserName != block && _unitOfWork.Blockings.Find(b => b.UserId == currentUser.Id && b.BlockedUserId == blockedUser.Id).FirstOrDefault() == null)
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
            User currentUser = CurrentUser;
            User blockedUser = _unitOfWork.Users.Find(u => u.UserName == block).FirstOrDefault();
            BlackList blocking = _unitOfWork.Blockings.Find(b => b.UserId == currentUser.Id && b.BlockedUserId == blockedUser.Id).FirstOrDefault();

            if (currentUser != null && blockedUser != null && currentUser.UserName != block && blocking != null)
            {
                _unitOfWork.Blockings.Delete(blocking.Id);
                _unitOfWork.Save();
            }
        }
        public async Task DismissBlockAsync(string block)
        {
            User currentUser = CurrentUser;
            User blockedUser = _unitOfWork.Users.Find(u => u.UserName == block).FirstOrDefault();
            BlackList blocking = _unitOfWork.Blockings.Find(b => b.UserId == currentUser.Id && b.BlockedUserId == blockedUser.Id).FirstOrDefault();

            if (currentUser != null && blockedUser != null && currentUser.UserName != block && blocking != null)
            {
                await _unitOfWork.Blockings.DeleteAsync(blocking.Id);
                await _unitOfWork.SaveAsync();
            }
        }

        public ApplicationUser Create(string userName, string email, string password)
        {
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            if (_unitOfWork.IdentityUsers.Find(u => u.UserName == userName || u.Email == email).FirstOrDefault() != null)
                return null;

            ApplicationUser identity = new ApplicationUser
            {
                UserName = userName,
                Email = email,
                PhoneNumberConfirmed = false,
                EmailConfirmed = false,
                NormalizedEmail = email.ToUpper(),
                NormalizedUserName = userName.ToUpper(),
                LockoutEnabled = true,
                TwoFactorEnabled = false,
                SecurityStamp = userName.GetHashCode().ToString()
            };

            identity.PasswordHash = passwordHasher.HashPassword(identity, password);

            _unitOfWork.IdentityUsers.Create(identity);
            _unitOfWork.Users.Create(new User() { UserName = userName });
            _unitOfWork.Save();

            return identity;
        }
        public async Task<ApplicationUser> CreateAsync(string userName, string email, string password)
        {
            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            if (_unitOfWork.Users.Find(u => u.UserName == userName).FirstOrDefault() != null)
                return null;

            ApplicationUser identity = new ApplicationUser
            {
                UserName = userName,
                Email = email,
                PhoneNumberConfirmed = false,
                EmailConfirmed = false,
                NormalizedEmail = email.ToUpper(),
                NormalizedUserName = userName.ToUpper(),
                LockoutEnabled = true,
                TwoFactorEnabled = false,
                SecurityStamp = userName.GetHashCode().ToString()
            };

            identity.PasswordHash = passwordHasher.HashPassword(identity, password);

            await _unitOfWork.IdentityUsers.CreateAsync(identity);
            await _unitOfWork.Users.CreateAsync(new User() { UserName = userName });
            await _unitOfWork.SaveAsync();

            return identity;
        }

        public void Edit(string userName, string realName, string about, string webSite)
        {
            User user = _unitOfWork.Users.Find(u => u.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                if (user.RealName != realName)
                {
                    user.RealName = realName;
                    _unitOfWork.Users.Update(user);
                }

                if (user.About != about)
                {
                    user.About = about;
                    _unitOfWork.Users.Update(user);
                }

                if (user.WebSite != webSite)
                {
                    user.WebSite = webSite;
                    _unitOfWork.Users.Update(user);
                }

                _unitOfWork.Save();
            }
        }
        public async Task EditAsync(string userName, string realName, string about, string webSite)
        {
            User user = _unitOfWork.Users.Find(u => u.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                if (user.RealName != realName)
                {
                    user.RealName = realName;
                    _unitOfWork.Users.Update(user);
                }
                
                if (user.About != about)
                {
                    user.About = about;
                    _unitOfWork.Users.Update(user);
                }

                if (user.WebSite != webSite)
                {
                    user.WebSite = webSite;
                    _unitOfWork.Users.Update(user);
                }

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