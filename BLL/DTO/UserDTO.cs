using System;

namespace PhotoHub.BLL.DTO
{
    /// <summary>
    /// User data transfer object.
    /// Contains username, user real name, user avatar and other.
    /// </summary>
    public class UserDTO
    {
        #region Properties

        /// <summary>
        /// Gets and sets user real name.
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// Gets and sets username.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets and sets user avatar.
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Gets and sets user date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets and sets user gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets and sets user confirmed by amdmin state.
        /// </summary>
        public bool Confirmed { get; set; }

        /// <summary>
        /// Gets and sets user followed by current user state.
        /// </summary>
        public bool Followed { get; set; }

        /// <summary>
        /// Gets and sets user blocked by current user state.
        /// </summary>
        public bool Blocked { get; set; }

        /// <summary>
        /// Gets and sets user private account setting.
        /// </summary>
        public bool PrivateAccount { get; set; }

        /// <summary>
        /// Gets and sets user block current user state.
        /// </summary>
        public bool IBlocked { get; set; }

        #endregion
    }
}
