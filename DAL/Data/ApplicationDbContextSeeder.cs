using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using PhotoHub.DAL.Entities;

namespace PhotoHub.DAL.Data
{
    public class ApplicationDbContextSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        List<Filter> _filters = new List<Filter>
        {
            new Filter { Name = "pure" },
            new Filter { Name = "nineties" },
            new Filter { Name = "aden" },
            new Filter { Name = "brooklyn" },
            new Filter { Name = "earlybird" },
            new Filter { Name = "gingham" },
            new Filter { Name = "hudson" },
            new Filter { Name = "inkwell" },
            new Filter { Name = "lofi" },
            new Filter { Name = "perpetua" },
            new Filter { Name = "reyes" },
            new Filter { Name = "toaster" },
            new Filter { Name = "walden" },
            new Filter { Name = "xpro" }
        };
        List<ApplicationUser> _users = new List<ApplicationUser>(6)
        {
            new ApplicationUser()
            {
                RealName = "Max Mironenko",
                UserName = "Max_Mironenko",
                Email = "Max.Mironenko3@gmail.com",
                Avatar = "user1.jpg",
                About = "First User"
            },
            new ApplicationUser()
            {
                RealName = "Bogdan Kashuk",
                UserName = "Bogdan_Kashuk",
                Email = "Bogdan.Kashuk@gmail.com",
                Avatar = "user2.jpg",
                About = "Second User"
            },
            new ApplicationUser()
            {
                RealName = "Den Natalin",
                UserName = "Den_Natalin",
                Email = "Den.Natalin@gmail.com",
                Avatar = "user3.jpg",
                About = "Third User"
            },
            new ApplicationUser()
            {
                RealName = "Dima Nabereznii",
                UserName = "Dima_Nabereznii",
                Email = "Dima.Nabereznii@gmail.com",
                Avatar = "user4.png",
                About = "Fourth User"
            },
            new ApplicationUser()
            {
                RealName = "Nick Gupal",
                UserName = "Nick_Gupal",
                Email = "Nick.Gupal@gmail.com",
                Avatar = "user5.jpg",
                About = "Fivth User"
            },
            new ApplicationUser()
            {
                RealName = "Jeka Karpiv",
                UserName = "Jeka_Karpiv",
                Email = "Jeka.Karpiv@gmail.com",
                Avatar = "user6.jpg",
                About = "SixTh User"
            }
        };
        List<Photo> _photos = new List<Photo>(6)
        {
            new Photo { Path = "example1.jpg", FilterId = 7, Description = "First example" },
            new Photo { Path = "example2.jpg", FilterId = 1, Description = "Second example" },
            new Photo { Path = "example3.jpg", FilterId = 8, Description = "Third example" },
            new Photo { Path = "example4.jpg", FilterId = 2, Description = "Fourth example" },
            new Photo { Path = "example5.jpg", FilterId = 9, Description = "Fifth example" },
            new Photo { Path = "example1.jpg", FilterId = 11, Description = "Sixth example" }
        };

        public ApplicationDbContextSeeder(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            _context.AddRange(_filters);
            await _context.SaveChangesAsync();
            
            foreach (ApplicationUser u in _users)
                await _userManager.CreateAsync(u, "somehash123A");
            await _context.SaveChangesAsync();

            for (int i = 0; i < 6; i++)
                _photos[i].OwnerId = _users[i].Id;

            _context.AddRange(_photos);
            await _context.SaveChangesAsync();
        }
    }
}
