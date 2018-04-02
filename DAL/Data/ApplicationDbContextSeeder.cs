#region using System/Microsoft
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
#endregion
using PhotoHub.DAL.Entities;

namespace PhotoHub.DAL.Data
{
    public class ApplicationDbContextSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        #region entities to seed
        List<Filter> _filters = new List<Filter>(14)
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
        List<Aperture> _apertures = new List<Aperture>(48)
        {
            new Aperture() {Name="Unknown"},
            new Aperture(){Name = "f/0.7"},
            new Aperture(){Name = "f/1.0"},
            new Aperture(){Name = "f/1.1"},
            new Aperture(){Name = "f/1.2"},
            new Aperture(){Name = "f/1.4"},
            new Aperture(){Name = "f/1.6"},
            new Aperture(){Name = "f/1.7"},
            new Aperture(){Name = "f/1.8"},
            new Aperture(){Name = "f/2"},
            new Aperture(){Name = "f/2.2"},
            new Aperture(){Name = "f/2.4"},
            new Aperture(){Name = "f/2.5"},
            new Aperture(){Name = "f/2.8"},
            new Aperture(){Name = "f/3.2"},
            new Aperture(){Name = "f/3.3"},
            new Aperture(){Name = "f/3.5"},
            new Aperture(){Name = "f/4"},
            new Aperture(){Name = "f/4.5"},
            new Aperture(){Name = "f/4.8"},
            new Aperture(){Name = "f/5"},
            new Aperture(){Name = "f/5.6"},
            new Aperture(){Name = "f/6.3"},
            new Aperture(){Name = "f/6.7"},
            new Aperture(){Name = "f/7.1"},
            new Aperture(){Name = "f/8"},
            new Aperture(){Name = "f/9"},
            new Aperture(){Name = "f/9.5"},
            new Aperture(){Name = "f/10"},
            new Aperture(){Name = "f/11"},
            new Aperture(){Name = "f/13"},
            new Aperture(){Name = "f/14"},
            new Aperture(){Name = "f/16"},
            new Aperture(){Name = "f/18"},
            new Aperture(){Name = "f/19"},
            new Aperture(){Name = "f/20"},
            new Aperture(){Name = "f/22"},
            new Aperture(){Name = "f/25"},
            new Aperture(){Name = "f/27"},
            new Aperture(){Name = "f/29"},
            new Aperture(){Name = "f/32"},
            new Aperture(){Name = "f/36"},
            new Aperture(){Name = "f/38"},
            new Aperture(){Name = "f/40"},
            new Aperture(){Name = "f/45"},
            new Aperture(){Name = "f/51"},
            new Aperture(){Name = "f/54"},
            new Aperture(){Name = "f/57"},
            new Aperture(){Name = "f/64"}
        };
        List<Exposure> _exposures = new List<Exposure>(68)
        {
            new Exposure() { Name = "Unknown" },
            new Exposure() { Name = "1/8000" },
            new Exposure() { Name = "1/6400" },
            new Exposure() { Name = "1/6000" },
            new Exposure() { Name = "1/5000" },
            new Exposure() { Name = "1/4000" },
            new Exposure() { Name = "1/3200" },
            new Exposure() { Name = "1/3000" },
            new Exposure() { Name = "1/2500" },
            new Exposure() { Name = "1/2000" },
            new Exposure() { Name = "1/1600" },
            new Exposure() { Name = "1/1500" },
            new Exposure() { Name = "1/1250" },
            new Exposure() { Name = "1/1000" },
            new Exposure() { Name = "1/800" },
            new Exposure() { Name = "1/750" },
            new Exposure() { Name = "1/640" },
            new Exposure() { Name = "1/500" },
            new Exposure() { Name = "1/400" },
            new Exposure() { Name = "1/350" },
            new Exposure() { Name = "1/320" },
            new Exposure() { Name = "1/250" },
            new Exposure() { Name = "1/200" },
            new Exposure() { Name = "1/180" },
            new Exposure() { Name = "1/160" },
            new Exposure() { Name = "1/125" },
            new Exposure() { Name = "1/100" },
            new Exposure() { Name = "1/90" },
            new Exposure() { Name = "1/80" },
            new Exposure() { Name = "1/60" },
            new Exposure() { Name = "1/50" },
            new Exposure() { Name = "1/45" },
            new Exposure() { Name = "1/40" },
            new Exposure() { Name = "1/30" },
            new Exposure() { Name = "1/25" },
            new Exposure() { Name = "1/20" },
            new Exposure() { Name = "1/15" },
            new Exposure() { Name = "1/13" },
            new Exposure() { Name = "1/10" },
            new Exposure() { Name = "1/8" },
            new Exposure() { Name = "1/6" },
            new Exposure() { Name = "1/5" },
            new Exposure() { Name = "1/4" },
            new Exposure() { Name = "0.3" },
            new Exposure() { Name = "0.4" },
            new Exposure() { Name = "0.5" },
            new Exposure() { Name = "0.6" },
            new Exposure() { Name = "0.7" },
            new Exposure() { Name = "0.8" },
            new Exposure() { Name = "1" },
            new Exposure() { Name = "1.3" },
            new Exposure() { Name = "1.5" },
            new Exposure() { Name = "1.6" },
            new Exposure() { Name = "2" },
            new Exposure() { Name = "2.5" },
            new Exposure() { Name = "3" },
            new Exposure() { Name = "3.2" },
            new Exposure() { Name = "4" },
            new Exposure() { Name = "5" },
            new Exposure() { Name = "6" },
            new Exposure() { Name = "8" },
            new Exposure() { Name = "10" },
            new Exposure() { Name = "13" },
            new Exposure() { Name = "15" },
            new Exposure() { Name = "20" },
            new Exposure() { Name = "25" },
            new Exposure() { Name = "30" }
        };
        List<ISO> _isos = new List<ISO>(30)
        {
            new ISO() { Name = "Unknown" },
            new ISO() { Name = "100" },
            new ISO() { Name = "125" },
            new ISO() { Name = "160" },
            new ISO() { Name = "200" },
            new ISO() { Name = "250" },
            new ISO() { Name = "320" },
            new ISO() { Name = "400" },
            new ISO() { Name = "500" },
            new ISO() { Name = "640" },
            new ISO() { Name = "800" },
            new ISO() { Name = "1000" },
            new ISO() { Name = "1250" },
            new ISO() { Name = "1600" },
            new ISO() { Name = "2000" },
            new ISO() { Name = "2500" },
            new ISO() { Name = "3200" },
            new ISO() { Name = "4000" },
            new ISO() { Name = "5000" },
            new ISO() { Name = "6400" },
            new ISO() { Name = "8000" },
            new ISO() { Name = "10,000" },
            new ISO() { Name = "12,800" },
            new ISO() { Name = "16,000" },
            new ISO() { Name = "20,000" },
            new ISO() { Name = "25,600" },
            new ISO() { Name = "51,200" },
            new ISO() { Name = "102,400" },
            new ISO() { Name = "204,800" },
            new ISO() { Name = "409,600" },
        };
        List<Photo> _photos = new List<Photo>(6)
        {
            new Photo { Path = "example1.jpg", FilterId = 7, Description = "First example", IsoId = 2, ApertureId = 2, ExposureId = 2, FocalLength = 22 },
            new Photo { Path = "example2.jpg", FilterId = 1, Description = "Second example", IsoId = 3, ApertureId = 3, ExposureId = 3, FocalLength = 18 },
            new Photo { Path = "example3.jpg", FilterId = 8, Description = "Third example", IsoId = 4, ApertureId = 4, ExposureId = 4, FocalLength = 16 },
            new Photo { Path = "example4.jpg", FilterId = 2, Description = "Fourth example", IsoId = 5, ApertureId = 5, ExposureId = 5, FocalLength = 20 },
            new Photo { Path = "example5.jpg", FilterId = 9, Description = "Fifth example", IsoId = 6, ApertureId = 6, ExposureId = 6, FocalLength = 21 },
            new Photo { Path = "example1.jpg", FilterId = 11, Description = "Sixth example", IsoId = 7, ApertureId = 7, ExposureId = 7, FocalLength = 14 }
        };
        List<Tag> _tags = new List<Tag>(7)
        {
            new Tag { Name = "HiRes" },
            new Tag { Name = "Widescreen" },
            new Tag { Name = "Horizontal" },
            new Tag { Name = "Vertical" },
            new Tag { Name = "Space" },
            new Tag { Name = "Nature" },
            new Tag { Name = "City" },
        };
        #endregion

        public ApplicationDbContextSeeder(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            _context.AddRange(_filters);
            await _context.SaveChangesAsync();
            _context.AddRange(_isos);
            await _context.SaveChangesAsync();
            _context.AddRange(_exposures);
            await _context.SaveChangesAsync();
            _context.AddRange(_apertures);
            await _context.SaveChangesAsync();
            _context.AddRange(_tags);
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
