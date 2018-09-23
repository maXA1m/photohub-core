using Microsoft.AspNetCore.Identity;

namespace PhotoHub.DAL.Entities
{
    /// <summary>
    /// Application user entity.
    /// Contains base identity properties.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUser"/> and calls <see cref="IdentityUser"/> .ctor.
        /// </summary>
        public ApplicationUser() : base()
        {

        }

        #endregion
    }
}
