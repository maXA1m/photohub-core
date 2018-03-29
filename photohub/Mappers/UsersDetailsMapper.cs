#region using System
using System;
using System.Collections.Generic;
#endregion
#region using PhotoHub.BLL
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
#endregion
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Mappers
{
    public class UsersDetailsMapper : IMapper<UserDetailsViewModel, UserDetailsDTO>
    {
        private readonly UsersMapper _usersMapper;

        public UsersDetailsMapper() => _usersMapper = new UsersMapper();

        public UserDetailsViewModel Map(UserDetailsDTO item)
        {
            return new UserDetailsViewModel()
            {
                RealName = item.RealName,
                UserName = item.UserName,
                Email = item.Email,
                Avatar = item.Avatar != null ? String.Format("/data/avatars/{0}/{1}", item.UserName, item.Avatar) : "/images/defaults/def-user-logo.png",
                About = item.About != null ? item.About : "Nothing to say",
                Date = item.Date.ToString("MMMM dd, yyyy"),
                Confirmed = item.Confirmed,
                Followed = item.Followed,
                Gender = item.Gender,
                WebSite = item.WebSite,
                Blocked = item.Blocked,
                PrivateAccount = item.PrivateAccount,
                IBlocked = item.IBlocked,
                Followings = _usersMapper.MapRange(item.Followings),
                Followers = _usersMapper.MapRange(item.Followers),
                Mutuals = item.Mutuals != null?_usersMapper.MapRange(item.Mutuals):null
            };
        }

        public List<UserDetailsViewModel> MapRange(IEnumerable<UserDetailsDTO> items)
        {
            List<UserDetailsViewModel> users = new List<UserDetailsViewModel>();

            foreach (UserDetailsDTO item in items)
            {
                users.Add(new UserDetailsViewModel()
                {
                    RealName = item.RealName,
                    UserName = item.UserName,
                    Email = item.Email,
                    Avatar = item.Avatar != null ? String.Format("/data/avatars/{0}/{1}", item.UserName, item.Avatar) : "/images/defaults/def-user-logo.png",
                    About = item.About != null ? item.About : "Nothing to say",
                    Date = item.Date.ToString("MMMM dd, yyyy"),
                    Confirmed = item.Confirmed,
                    Followed = item.Followed,
                    Gender = item.Gender,
                    WebSite = item.WebSite,
                    Blocked = item.Blocked,
                    PrivateAccount = item.PrivateAccount,
                    IBlocked = item.IBlocked,
                    Followings = _usersMapper.MapRange(item.Followings),
                    Followers = _usersMapper.MapRange(item.Followers),
                    Mutuals = item.Mutuals != null ? _usersMapper.MapRange(item.Mutuals) : null
                });
            }

            return users;
        }
    }
}
