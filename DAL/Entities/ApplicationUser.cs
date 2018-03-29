#region using System/Microsoft
using System;
using Microsoft.AspNetCore.Identity;
#endregion

namespace PhotoHub.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string RealName { get; set; }
        public string Avatar { get; set; }
        public string About { get; set; }
        public DateTime Date { get; set; }
        public string WebSite { get; set; }
        public string Gender { get; set; }
        public bool PrivateAccount { get; set; }

        //public int PinnedPhotoId { get; set; }
        //public Photo PinnedPhoto { get; set; }

        public ApplicationUser() : base()
        {

        }
    }
}
