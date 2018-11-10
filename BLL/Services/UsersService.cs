using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Entities;
using PhotoHub.BLL.Interfaces;
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Extensions;

namespace PhotoHub.BLL.Services
{
    /// <summary>
    /// Contains methods with users processing logic.
    /// Realization of <see cref="IUsersService"/>.
    /// </summary>
    public class UsersService : IUsersService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICurrentUserService _currentUserService;

        private bool _isDisposed;

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersService"/>.
        /// </summary>
        public UsersService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _currentUserService = new CurrentUserService(unitOfWork, httpContextAccessor);
        }

        #endregion

        #region Logic

        /// <summary>
        /// Loads all users with paggination, returns collection of user DTOs.
        /// </summary>
        public IEnumerable<UserDTO> GetAll(int page, int pageSize)
        {
            var users = _unitOfWork.Users.GetAll(page, pageSize);
            var userDTOs = new List<UserDTO>(users.Count());

            foreach (var user in users)
            {
                userDTOs.Add(MapUser(user));
            }

            return userDTOs;
        }

        /// <summary>
        /// Loads user by username, returns user DTO.
        /// </summary>
        public UserDetailsDTO Get(string userName)
        {
            var user = _unitOfWork.Users.Find(u => u.UserName == userName).FirstOrDefault();

            return MapUserDetails(user);
        }

        /// <summary>
        /// Loads blocked users by this user.
        /// </summary>
        public IEnumerable<UserDTO> GetBlocked(int page, int pageSize)
        {
            var blacklists = _unitOfWork.Blockings.Find(b => b.UserId == _currentUserService.CurrentUser.Id);
            var users = new List<User>();

            foreach(var blocking in blacklists)
            {
                users.Add(blocking.BlockedUser);
            }

            var userDTOs = new List<UserDTO>(pageSize);

            foreach (var user in users)
            {
                userDTOs.Add(MapUser(user));
            }

            return userDTOs;
        }

        /// <summary>
        /// Method for searching users.
        /// </summary>
        public IEnumerable<UserDTO> Search(int page, string search, int pageSize)
        {
            var currentUser = _currentUserService.CurrentUser;
            IEnumerable<User> users;

            if (string.IsNullOrEmpty(search))
            {
                users = _unitOfWork.Users.Find(u => u.UserName != currentUser.UserName).OrderByDescending(u => u.Date).Skip(page * pageSize).Take(pageSize);
            }
            else
            {
                users = _unitOfWork.Users.Find(u =>
                    u.UserName != currentUser.UserName &&
                    (
                        u.UserName.ToLower().Contains(search.ToLower()) || (!string.IsNullOrEmpty(u.RealName) ? u.RealName.ToLower().Contains(search.ToLower()) : false)
                    )
                ).OrderBy(u => u.Date).Skip(page * pageSize).Take(pageSize);
            }

            var userDTOs = new List<UserDTO>();

            foreach(var user in users)
            {
                userDTOs.Add(MapUser(user));
            }

            return userDTOs;
        }

        /// <summary>
        /// Follows user by current user.
        /// </summary>
        public void Follow(string follow)
        {
            var currentUser = _currentUserService.CurrentUser;
            var followedUser = _unitOfWork.Users.Find(u => u.UserName == follow).FirstOrDefault();

            if (currentUser != null && followedUser != null && currentUser.UserName != follow && _unitOfWork.Followings.Find(f => f.UserId == currentUser.Id && f.FollowedUserId == followedUser.Id).FirstOrDefault() == null)
            {
                _unitOfWork.Followings.Create(new Following
                {
                    UserId = currentUser.Id,
                    FollowedUserId = followedUser.Id
                });

                _unitOfWork.Save();
            }
        }

        /// <summary>
        /// Async follows user by current user.
        /// </summary>
        public async Task FollowAsync(string follow)
        {
            var currentUser = _currentUserService.CurrentUser;
            var followedUser = _unitOfWork.Users.Find(u => u.UserName == follow).FirstOrDefault();

            if (currentUser != null && followedUser != null && currentUser.UserName != follow && _unitOfWork.Followings.Find(f => f.UserId == currentUser.Id && f.FollowedUserId == followedUser.Id).FirstOrDefault() == null)
            {
                await _unitOfWork.Followings.CreateAsync(new Following
                {
                    UserId = currentUser.Id,
                    FollowedUserId = followedUser.Id
                });

                await _unitOfWork.SaveAsync();
            }
        }

        /// <summary>
        /// Dismiss following on user by current user.
        /// </summary>
        public void DismissFollow(string follow)
        {
            var currentUser = _currentUserService.CurrentUser;
            var followedUser = _unitOfWork.Users.Find(u => u.UserName == follow).FirstOrDefault();
            var following = _unitOfWork.Followings.Find(f => f.UserId == currentUser.Id && f.FollowedUserId == followedUser.Id).FirstOrDefault();

            if (currentUser != null && followedUser != null && currentUser.UserName != follow && following != null)
            {
                _unitOfWork.Followings.Delete(following.Id);

                _unitOfWork.Save();
            }
        }

        /// <summary>
        /// Async dismiss following on user by current user.
        /// </summary>
        public async Task DismissFollowAsync(string follow)
        {
            var currentUser = _currentUserService.CurrentUser;
            var followedUser = _unitOfWork.Users.Find(u => u.UserName == follow).FirstOrDefault();
            var following = _unitOfWork.Followings.Find(f => f.UserId == currentUser.Id && f.FollowedUserId == followedUser.Id).FirstOrDefault();

            if (currentUser != null && followedUser != null && currentUser.UserName != follow && following != null)
            {
                await _unitOfWork.Followings.DeleteAsync(following.Id);

                await _unitOfWork.SaveAsync();
            }
        }

        /// <summary>
        /// Blocks user by current user.
        /// </summary>
        public void Block(string block)
        {
            var currentUser = _currentUserService.CurrentUser;
            var blockedUser = _unitOfWork.Users.Find(u => u.UserName == block).FirstOrDefault();

            if (currentUser != null && blockedUser != null && currentUser.UserName != block && _unitOfWork.Blockings.Find(b => b.UserId == currentUser.Id && b.BlockedUserId == blockedUser.Id).FirstOrDefault() == null)
            {
                _unitOfWork.Blockings.Create(new BlackList
                {
                    UserId = currentUser.Id,
                    BlockedUserId = blockedUser.Id
                });

                _unitOfWork.Save();
            }
        }

        /// <summary>
        /// Async blocks user by current user.
        /// </summary>
        public async Task BlockAsync(string block)
        {
            var currentUser = _currentUserService.CurrentUser;
            var blockedUser = _unitOfWork.Users.Find(u => u.UserName == block).FirstOrDefault();

            if (currentUser != null && blockedUser != null && currentUser.UserName != block && _unitOfWork.Blockings.Find(b => b.UserId == currentUser.Id && b.BlockedUserId == blockedUser.Id).FirstOrDefault() == null)
            {
                await _unitOfWork.Blockings.CreateAsync(new BlackList
                {
                    UserId = currentUser.Id,
                    BlockedUserId = blockedUser.Id
                });

                await _unitOfWork.SaveAsync();
            }
        }

        /// <summary>
        /// Dismiss blocking user by current user.
        /// </summary>
        public void DismissBlock(string block)
        {
            var currentUser = _currentUserService.CurrentUser;
            var blockedUser = _unitOfWork.Users.Find(u => u.UserName == block).FirstOrDefault();
            var blocking = _unitOfWork.Blockings.Find(b => b.UserId == currentUser.Id && b.BlockedUserId == blockedUser.Id).FirstOrDefault();

            if (currentUser != null && blockedUser != null && currentUser.UserName != block && blocking != null)
            {
                _unitOfWork.Blockings.Delete(blocking.Id);
                _unitOfWork.Save();
            }
        }

        /// <summary>
        /// Async dismiss blocking user by current user.
        /// </summary>
        public async Task DismissBlockAsync(string block)
        {
            var currentUser = _currentUserService.CurrentUser;
            var blockedUser = _unitOfWork.Users.Find(u => u.UserName == block).FirstOrDefault();
            var blocking = _unitOfWork.Blockings.Find(b => b.UserId == currentUser.Id && b.BlockedUserId == blockedUser.Id).FirstOrDefault();

            if (currentUser != null && blockedUser != null && currentUser.UserName != block && blocking != null)
            {
                await _unitOfWork.Blockings.DeleteAsync(blocking.Id);
                await _unitOfWork.SaveAsync();
            }
        }

        /// <summary>
        /// Reports user by current user.
        /// </summary>
        public void Report(string userName, string text)
        {
            var currentUser = _currentUserService.CurrentUser;
            var reportedUser = _unitOfWork.Users.Find(u => u.UserName == userName).FirstOrDefault();

            if (currentUser != null && reportedUser != null && currentUser.UserName != userName && _unitOfWork.UserReports.Find(b => b.UserId == currentUser.Id && b.ReportedUserId == reportedUser.Id).FirstOrDefault() == null)
            {
                _unitOfWork.UserReports.Create(new UserReport
                {
                    UserId = currentUser.Id,
                    ReportedUserId = reportedUser.Id,
                    Text = text
                });

                _unitOfWork.Save();
            }
        }

        /// <summary>
        /// Async reports user by current user.
        /// </summary>
        public async Task ReportAsync(string userName, string text)
        {
            var currentUser = _currentUserService.CurrentUser;
            var reportedUser = _unitOfWork.Users.Find(u => u.UserName == userName).FirstOrDefault();

            if (currentUser != null && reportedUser != null && currentUser.UserName != userName && _unitOfWork.UserReports.Find(b => b.UserId == currentUser.Id && b.ReportedUserId == reportedUser.Id).FirstOrDefault() == null)
            {
                _unitOfWork.UserReports.Create(new UserReport
                {
                    UserId = currentUser.Id,
                    ReportedUserId = reportedUser.Id,
                    Text = text
                });

                await _unitOfWork.SaveAsync();
            }
        }

        /// <summary>
        /// Creates user.
        /// </summary>
        public ApplicationUser Create(string userName, string email, string password)
        {
            if (_unitOfWork.IdentityUsers.Find(u => u.UserName == userName || u.Email == email).FirstOrDefault() != null)
            {
                return null;
            }

            var identity = new ApplicationUser
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

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            identity.PasswordHash = passwordHasher.HashPassword(identity, password);

            _unitOfWork.IdentityUsers.Create(identity);
            _unitOfWork.Users.Create(new User { UserName = userName });
            _unitOfWork.Save();

            return identity;
        }

        /// <summary>
        /// Async creates user.
        /// </summary>
        public async Task<ApplicationUser> CreateAsync(string userName, string email, string password)
        {
            if (_unitOfWork.Users.Find(u => u.UserName == userName).FirstOrDefault() != null)
            {
                return null;
            }

            var identity = new ApplicationUser
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

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            identity.PasswordHash = passwordHasher.HashPassword(identity, password);

            await _unitOfWork.IdentityUsers.CreateAsync(identity);
            await _unitOfWork.Users.CreateAsync(new User { UserName = userName });
            await _unitOfWork.SaveAsync();

            return identity;
        }

        /// <summary>
        /// Edits user.
        /// </summary>
        public void Edit(string userName, string realName, string about, string webSite, string gender, string avatar = null)
        {
            var user = _unitOfWork.Users.Find(u => u.UserName == userName).FirstOrDefault();

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

                if(user.Gender != gender)
                {
                    user.Gender = gender;
                    _unitOfWork.Users.Update(user);
                }

                if (!string.IsNullOrEmpty(avatar))
                {
                    user.Avatar = avatar;
                    _unitOfWork.Users.Update(user);
                }

                _unitOfWork.Save();
            }
        }

        /// <summary>
        /// Async edits user.
        /// </summary>
        public async Task EditAsync(string userName, string realName, string about, string webSite, string gender, string avatar = null)
        {
            var user = _unitOfWork.Users.Find(u => u.UserName == userName).FirstOrDefault();

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

                if (user.Gender != gender)
                {
                    user.Gender = gender;
                    _unitOfWork.Users.Update(user);
                }

                if (!string.IsNullOrEmpty(avatar))
                {
                    user.Avatar = avatar;
                    _unitOfWork.Users.Update(user);
                }

                await _unitOfWork.SaveAsync();
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Helps map user entity to user data transfer object.
        /// </summary>
        protected UserDTO MapUser(User user)
        {
            var currentUser = _currentUserService.CurrentUser;

            return user.ToDTO(_unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null,
                              _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == currentUser.Id).FirstOrDefault() != null,
                              _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == currentUser.Id).FirstOrDefault() != null,
                              _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null);
        }

        /// <summary>
        /// Helps map user details entity to user details data transfer object.
        /// </summary>
        protected UserDetailsDTO MapUserDetails(User user)
        {
            var currentUser = _currentUserService.CurrentUser;

            var followings = new List<UserDTO>();
            var followers = new List<UserDTO>();

            if (currentUser == null)
            {
                foreach (var following in _unitOfWork.Followings.Find(f => f.UserId == user.Id))
                {
                    followings.Add(following.FollowedUser
                        .ToDTO(_unitOfWork.Confirmations.Find(c => c.UserId == following.FollowedUserId).FirstOrDefault() != null, false, false, false));
                }

                foreach (var follower in _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id))
                {
                    followers.Add(follower.User
                        .ToDTO(_unitOfWork.Confirmations.Find(c => c.UserId == follower.UserId).FirstOrDefault() != null, false, false, false));
                }

                return user.ToDTO(_unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null, 
                    false, false, false, followings, followers, null);
            }
            else
            {
                foreach (var following in _unitOfWork.Followings.Find(f => f.UserId == user.Id))
                {
                    followings.Add(following.FollowedUser
                        .ToDTO(_unitOfWork.Confirmations.Find(c => c.UserId == following.FollowedUserId).FirstOrDefault() != null,
                               _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null,
                               _unitOfWork.Followings.Find(f => f.FollowedUserId == following.FollowedUserId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                               _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null));
                }

                foreach (var follower in _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id))
                {
                    followers.Add(follower.User
                        .ToDTO(_unitOfWork.Confirmations.Find(c => c.UserId == follower.UserId).FirstOrDefault() != null,
                               _unitOfWork.Followings.Find(f => f.FollowedUserId == follower.UserId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                               _unitOfWork.Blockings.Find(b => b.BlockedUserId == follower.UserId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                               _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null));
                }

                var mutuals = new List<UserDTO>();

                if (followers.Count > 0)
                {
                    foreach (var follower in _unitOfWork.Followings.Find(f => f.FollowedUserId == currentUser.Id))
                    {
                        if (follower.UserId != user.Id && _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == follower.UserId).FirstOrDefault() != null)
                        {
                            mutuals.Add(follower.User
                                .ToDTO(
                                _unitOfWork.Confirmations.Find(c => c.UserId == follower.UserId).FirstOrDefault() != null,
                                _unitOfWork.Followings.Find(f => f.FollowedUserId == follower.UserId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                                _unitOfWork.Blockings.Find(b => b.BlockedUserId == follower.UserId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                                _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null));
                        }
                    }
                }

                return user.ToDTO(_unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null,
                                  _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == currentUser.Id).FirstOrDefault() != null,
                                  _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == currentUser.Id).FirstOrDefault() != null,
                                  _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == user.Id).FirstOrDefault() != null,
                                  followings, followers, mutuals);
            }
        }

        #endregion

        #region Disposing

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                    _currentUserService.Dispose();
                }

                _isDisposed = true;
            }
        }

        ~UsersService()
        {
            Dispose(false);
        }

        #endregion
    }
}