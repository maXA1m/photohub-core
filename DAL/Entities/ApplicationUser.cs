using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhotoHub.DAL.Data;

namespace PhotoHub.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        //public string RealName { get; set; }
        public string Avatar { get; set; }
        public string About { get; set; }
        public DateTime Date { get; set; }

        public ApplicationUser() : base()
        {

        }
    }
}
