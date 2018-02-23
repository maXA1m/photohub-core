using System.Collections.Generic;

namespace PhotoHub.BLL.DTO
{
    public class UserDetailsDTO : UserDTO
    {
        public string About { get; set; }

        public ICollection<UserDTO> Followings { get; set; }
        public ICollection<UserDTO> Followers { get; set; }
    }
}
