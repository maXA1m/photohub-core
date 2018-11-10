using System.Collections.Generic;

namespace PhotoHub.BLL.DTO
{
    /// <summary>
    /// User details data transfer object.
    /// Inherits properties from user data transfer object, also contains user about info, user website, mutuals collection, followings collection, followers collection.
    /// </summary>
    public class UserDetailsDTO : UserDTO
    {
        /// <summary>
        /// Gets and sets user about.
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Gets and sets user website.
        /// </summary>
        public string WebSite { get; set; }

        /// <summary>
        /// Gets and sets mutuals collection of mutual user DTOs.
        /// </summary>
        public IEnumerable<UserDTO> Mutuals { get; set; }

        /// <summary>
        /// Gets and sets followings collection of user following DTOs.
        /// </summary>
        public IEnumerable<UserDTO> Followings { get; set; }

        /// <summary>
        /// Gets and sets followers collection of user follower DTOs.
        /// </summary>
        public IEnumerable<UserDTO> Followers { get; set; }
    }
}
