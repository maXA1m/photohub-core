using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PhotoHub.DAL.Entities;

namespace PhotoHub.DAL.Data
{
    /// <summary>
    /// Seeder for main DB context in the application.
    /// </summary>
    public class ApplicationDbContextSeeder
    {
        #region Fields

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly List<Filter> _filters = new List<Filter>
        {
            new Filter
            {
                Name = "pure"
            },
            new Filter
            {
                Name = "nineties"
            },
            new Filter
            {
                Name = "aden"
            },
            new Filter
            {
                Name = "brooklyn"
            },
            new Filter
            {
                Name = "earlybird"
            },
            new Filter
            {
                Name = "gingham"
            },
            new Filter
            {
                Name = "hudson"
            },
            new Filter
            {
                Name = "inkwell"
            },
            new Filter
            {
                Name = "lofi"
            },
            new Filter
            {
                Name = "perpetua"
            },
            new Filter
            {
                Name = "reyes"
            },
            new Filter
            {
                Name = "toaster"
            },
            new Filter
            {
                Name = "walden"
            },
            new Filter
            {
                Name = "xpro"
            }
        };
        private readonly List<ApplicationUser> _identity = new List<ApplicationUser>
        {
            new ApplicationUser
            {
                UserName = "Max_Mironenko",
                Email = "Max.Mironenko3@gmail.com"
            },
            new ApplicationUser
            {
                UserName = "Bogdan_Kashuk",
                Email = "Bogdan.Kashuk@gmail.com"
            },
            new ApplicationUser
            {
                UserName = "Den_Natalin",
                Email = "Den.Natalin@gmail.com"
            },
            new ApplicationUser
            {
                UserName = "Dima_Nabereznii",
                Email = "Dima.Nabereznii@gmail.com"
            },
            new ApplicationUser
            {
                UserName = "Nick_Gupal",
                Email = "Nick.Gupal@gmail.com"
            },
            new ApplicationUser
            {
                UserName = "Jeka_Karpiv",
                Email = "Jeka.Karpiv@gmail.com"
            },
            new ApplicationUser
            {
                UserName = "photohub_admin",
                Email = "photohub.admin@gmail.com"
            }
        };
        private readonly List<User> _users = new List<User>
        {
            new User
            {
                RealName = "Max Mironenko",
                UserName = "Max_Mironenko",
                Avatar = "user1.jpg",
                About = "First User"
            },
            new User
            {
                RealName = "Bogdan Kashuk",
                UserName = "Bogdan_Kashuk",
                Avatar = "user2.jpg",
                About = "Second User"
            },
            new User
            {
                RealName = "Den Natalin",
                UserName = "Den_Natalin",
                Avatar = "user3.jpg",
                About = "Third User"
            },
            new User
            {
                RealName = "Dima Nabereznii",
                UserName = "Dima_Nabereznii",
                Avatar = "user4.png",
                About = "Fourth User"
            },
            new User
            {
                RealName = "Nick Gupal",
                UserName = "Nick_Gupal",
                Avatar = "user5.jpg",
                About = "Fivth User"
            },
            new User
            {
                RealName = "Jeka Karpiv",
                UserName = "Jeka_Karpiv",
                Avatar = "user6.jpg",
                About = "SixTh User"
            },
            new User//admin
            {
                RealName = "PhotoHub Admin",
                UserName = "photohub_admin",
                About = "Official admin"
            }
        };
        private readonly List<Photo> _photos = new List<Photo>
        {
            new Photo
            {
                Path = "example1.jpg",
                FilterId = 7,
                Description = "First example",
                Iso = 2000,
                Aperture = 16,
                Exposure = 0.00625,
                FocalLength = 22
            },
            new Photo
            {
                Path = "example2.jpg",
                FilterId = 1,
                Description = "Second example",
                Iso = 4000, Aperture = 4.8,
                Exposure = 0.00625,
                FocalLength = 18
            },
            new Photo
            {
                Path = "example3.jpg",
                FilterId = 8,
                Description = "Third example",
                Iso = 4000, Aperture = 8,
                Exposure = 0.00625,
                FocalLength = 16
            },
            new Photo
            {
                Path = "example4.jpg",
                FilterId = 2,
                Description = "Fourth example",
                Iso = 50000, Aperture = 12,
                Exposure = 0.00625,
                FocalLength = 20
            },
            new Photo
            {
                Path = "example5.jpg",
                FilterId = 9,
                Description = "Fifth example",
                Iso = 6000, Aperture = 16,
                Exposure = 0.00625,
                FocalLength = 21
            },
            new Photo
            {
                Path = "example1.jpg",
                FilterId = 11,
                Description = "Sixth example",
                Iso = 70000, Aperture = 12,
                Exposure = 0.00625,
                FocalLength = 14
            }
        };
        private readonly List<Tag> _tags = new List<Tag>
        {
            new Tag
            {
                Name = "sunset"
            },
            new Tag
            {
                Name = "beach"
            },
            new Tag
            {
                Name = "water"
            },
            new Tag
            {
                Name = "sky"
            },
            new Tag
            {
                Name = "red"
            },
            new Tag
            {
                Name = "flower"
            },
            new Tag
            {
                Name = "nature"
            },
            new Tag
            {
                Name = "blue"
            },
            new Tag
            {
                Name = "night"
            },
            new Tag
            {
                Name = "white"
            },
            new Tag
            {
                Name = "tree"
            },
            new Tag
            {
                Name = "green"
            },
            new Tag
            {
                Name = "flowers"
            },
            new Tag
            {
                Name = "portrait"
            },
            new Tag
            {
                Name = "art"
            },
            new Tag
            {
                Name = "light"
            },
            new Tag
            {
                Name = "snow"
            },
            new Tag
            {
                Name = "dog"
            },
            new Tag
            {
                Name = "cat"
            },
            new Tag
            {
                Name = "street"
            },
            new Tag
            {
                Name = "landscape"
            },
            new Tag
            {
                Name = "hires"
            },
            new Tag
            {
                Name = "widescreen"
            },
            new Tag
            {
                Name = "horizontal"
            },
            new Tag
            {
                Name = "vertical"
            },
            new Tag
            {
                Name = "space"
            },
            new Tag
            {
                Name = "nature"
            },
            new Tag
            {
                Name = "city"
            },
        };

        #endregion

        #region .ctors

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContextSeeder"/>.
        /// </summary>
        public ApplicationDbContextSeeder(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #endregion

        #region Logic

        /// <summary>
        /// Seeds database with default values of filters, photos, tags and users (identity too).
        /// </summary>
        public async Task Seed()
        {
            await _context.AddRangeAsync(_filters);
            await _context.SaveChangesAsync();

            await _context.AddRangeAsync(_tags);
            await _context.SaveChangesAsync();

            foreach (var u in _identity)
            {
                await _userManager.CreateAsync(u, "password123A");
            }

            await _context.SaveChangesAsync();
            
            await _context.AddRangeAsync(_users);
            await _context.SaveChangesAsync();

            for (var i = 0; i < 6; i++)
            {
                _photos[i].OwnerId = _users[i].Id;
            }

            _context.AddRange(_photos);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Creates user roles and first admin.
        /// </summary>
        public async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var adminRoleCheck = await RoleManager.RoleExistsAsync("Admin");

            if (!adminRoleCheck)
            {
                await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            ApplicationUser user = await UserManager.FindByNameAsync("photohub_admin");

            await UserManager.AddToRoleAsync(user, "Admin");
        }

        #endregion
    }
}
