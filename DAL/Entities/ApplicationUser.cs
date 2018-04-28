#region using System/Microsoft
using System;
using Microsoft.AspNetCore.Identity;
#endregion

namespace PhotoHub.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {

        }
    }
}
