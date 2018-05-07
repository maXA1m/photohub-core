using System;

namespace PhotoHub.DAL.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }//Key value equal to username in ApplicationUser
        public string RealName { get; set; }
        public string Avatar { get; set; }
        public string About { get; set; }
        public DateTime Date { get; set; }
        public string WebSite { get; set; }
        public string Gender { get; set; }
        public bool PrivateAccount { get; set; }

        public User()
        {
            Gender = "Male";
            PrivateAccount = false;
            Date = DateTime.Now;
        }
    }
}
