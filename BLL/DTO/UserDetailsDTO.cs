using System.Collections.Generic;

namespace PhotoHub.BLL.DTO
{
    public class UserDetailsDTO : UserDTO
    {
        public string About { get; set; }
        public string WebSite { get; set; }

        public IEnumerable<UserDTO> Mutuals { get; set; }
        public IEnumerable<UserDTO> Followings { get; set; }
        public IEnumerable<UserDTO> Followers { get; set; }
    }
}
