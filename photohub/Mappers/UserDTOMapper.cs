using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;
using System;
using System.Collections.Generic;

namespace PhotoHub.WEB.Mappers
{
    public static class UserDTOMapper
    {
        public static UserViewModel ToUserViewModel(UserDTO user)
        {
            return new UserViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar != null ? String.Format("/data/avatars/{0}/{1}", user.UserName, user.Avatar) : "/images/defaults/def-user-logo.png",
                Date = user.Date.ToString("MMMM dd, yyyy"),
                Confirmed = user.Confirmed,
                Followed = user.Followed,
                Blocked = user.Blocked,
                IBlocked = user.IBlocked
            };
        }
        public static List<UserViewModel> ToUserViewModels(IEnumerable<UserDTO> users)
        {
            List<UserViewModel> userViewModels = new List<UserViewModel>();

            foreach (UserDTO user in users)
            {
                userViewModels.Add(new UserViewModel()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Avatar = user.Avatar != null ? String.Format("/data/avatars/{0}/{1}", user.UserName, user.Avatar) : "/images/defaults/def-user-logo.png",
                    Date = user.Date.ToString("MMMM dd, yyyy"),
                    Confirmed = user.Confirmed,
                    Followed = user.Followed,
                    Blocked = user.Blocked,
                    IBlocked = user.IBlocked
                });
            }

            return userViewModels;
        }

        public static UserDetailsViewModel ToUserDetailsViewModel(UserDetailsDTO user)
        {
            return new UserDetailsViewModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                Avatar = user.Avatar != null ? String.Format("/data/avatars/{0}/{1}", user.UserName, user.Avatar) : "/images/defaults/def-user-logo.png",
                About = user.About != null ? user.About : "Nothing to say",
                Date = user.Date.ToString("MMMM dd, yyyy"),
                Confirmed = user.Confirmed,
                Followed = user.Followed,
                Blocked = user.Blocked,
                IBlocked = user.IBlocked,
                Followings = ToUserViewModels(user.Followings),
                Followers = ToUserViewModels(user.Followers),
            };
        }
        public static List<UserDetailsViewModel> ToUserDetailsViewModels(IEnumerable<UserDetailsDTO> users)
        {
            List<UserDetailsViewModel> userDetailsViewModels = new List<UserDetailsViewModel>();

            foreach (UserDetailsDTO user in users)
            {
                userDetailsViewModels.Add(new UserDetailsViewModel()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Avatar = user.Avatar != null ? String.Format("/data/avatars/{0}/{1}", user.UserName, user.Avatar) : "/images/defaults/def-user-logo.png",
                    About = user.About != null ? user.About : "Nothing to say",
                    Date = user.Date.ToString("MMMM dd, yyyy"),
                    Confirmed = user.Confirmed,
                    Followed = user.Followed,
                    Blocked = user.Blocked,
                    IBlocked = user.IBlocked,
                    Followings = ToUserViewModels(user.Followings),
                    Followers = ToUserViewModels(user.Followers),
                });
            }

            return userDetailsViewModels;
        }
    }
}