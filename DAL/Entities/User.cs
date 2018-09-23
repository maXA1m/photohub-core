using System;

namespace PhotoHub.DAL.Entities
{
    /// <summary>
    /// User entity (not identity).
    /// Contains user properties.
    /// <see cref="UserName"/> is key value equal to <see cref="UserName"/> in <see cref="ApplicationUser"/>.
    /// </summary>
    public class User : BaseEntity
    {
        #region Properties

        /// <summary>
        /// Gets and sets user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets and sets user real name.
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// Gets and sets user avatar.
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Gets and sets user about section.
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Gets and sets user date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets and sets user website link.
        /// </summary>
        public string WebSite { get; set; }

        /// <summary>
        /// Gets and sets user gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets and sets user private account config.
        /// </summary>
        public bool PrivateAccount { get; set; }

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/>, sets property to default values.
        /// </summary>
        public User()
        {
            Gender = "Male";
            PrivateAccount = false;
            Date = DateTime.Now;
        }

        #endregion
    }
}
