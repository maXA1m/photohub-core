using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
using PhotoHub.WEB.ViewModels;
using System;
using System.Collections.Generic;

namespace PhotoHub.WEB.Mappers
{
    public class UsersMapper : IMapper<UserViewModel, UserDTO>
    {
        public UserViewModel Map(UserDTO item)
        {
            return new UserViewModel()
            {
                RealName = item.RealName,
                UserName = item.UserName,
                Email = item.Email,
                Avatar = item.Avatar != null ? String.Format("/data/avatars/{0}/{1}", item.UserName, item.Avatar) : "/images/defaults/def-user-logo.png",
                Date = item.Date.ToString("MMMM dd, yyyy"),
                Confirmed = item.Confirmed,
                Followed = item.Followed,
                Blocked = item.Blocked,
                PrivateAccount = item.PrivateAccount,
                IBlocked = item.IBlocked
            };
        }
        public List<UserViewModel> MapRange(IEnumerable<UserDTO> items)
        {
            List<UserViewModel> users = new List<UserViewModel>();

            foreach (UserDTO item in items)
            {
                users.Add(new UserViewModel()
                {
                    RealName = item.RealName,
                    UserName = item.UserName,
                    Email = item.Email,
                    Avatar = item.Avatar != null ? String.Format("/data/avatars/{0}/{1}", item.UserName, item.Avatar) : "/images/defaults/def-user-logo.png",
                    Date = item.Date.ToString("MMMM dd, yyyy"),
                    Confirmed = item.Confirmed,
                    Followed = item.Followed,
                    Blocked = item.Blocked,
                    PrivateAccount = item.PrivateAccount,
                    IBlocked = item.IBlocked
                });
            }

            return users;
        }
    }
}