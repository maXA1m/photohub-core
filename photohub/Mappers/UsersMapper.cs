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
    public class UsersMapper : IMapper<UserViewModel, UserDTO>
    {
        public UserViewModel Map(UserDTO item)
        {
            if (item == null)
                return null;

            return new UserViewModel()
            {
                RealName = item.RealName,
                UserName = item.UserName,
                Avatar = item.Avatar != null ? String.Format("/data/avatars/{0}/{1}", item.UserName, item.Avatar) : String.IsNullOrEmpty(item.Gender) || item.Gender == "Male"? "/images/defaults/def-male-logo.png" : "/images/defaults/def-female-logo.png",
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
            if (items == null)
                return null;

            List<UserViewModel> users = new List<UserViewModel>();
            foreach (UserDTO item in items)
            {
                users.Add(new UserViewModel()
                {
                    RealName = item.RealName,
                    UserName = item.UserName,
                    Avatar = item.Avatar != null ? String.Format("/data/avatars/{0}/{1}", item.UserName, item.Avatar) : String.IsNullOrEmpty(item.Gender) || item.Gender == "Male" ? "/images/defaults/def-male-logo.png" : "/images/defaults/def-female-logo.png",
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