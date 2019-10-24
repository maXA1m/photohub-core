using PhotoHub.DAL.Entities;
using System.Collections.Generic;

namespace PhotoHub.DAL.Data
{
    internal static class Defaults
    {
        internal static class Strings
        {
            public const string AdminRole = "Admin";
            public const string AdminUserName = "photohub_admin";
            public const string IdentitiesPassword = "password123A";
        }

        internal static class Entities
        {
            public static List<Filter> Filters = new List<Filter>
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
            public static List<ApplicationUser> Identities = new List<ApplicationUser>
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
                    UserName = Strings.AdminUserName,
                    Email = "photohub.admin@gmail.com"
                }
            };
            public static List<User> Users = new List<User>
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
                new User
                {
                    RealName = "PhotoHub Admin",
                    UserName = Strings.AdminUserName,
                    About = "Official admin"
                }
            };
            public static List<Photo> Photos = new List<Photo>
            {
                new Photo
                {
                    OwnerId = 1,
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
                    OwnerId = 2,
                    Path = "example2.jpg",
                    FilterId = 1,
                    Description = "Second example",
                    Iso = 4000, Aperture = 4.8,
                    Exposure = 0.00625,
                    FocalLength = 18
                },
                new Photo
                {
                    OwnerId = 3,
                    Path = "example3.jpg",
                    FilterId = 8,
                    Description = "Third example",
                    Iso = 4000, Aperture = 8,
                    Exposure = 0.00625,
                    FocalLength = 16
                },
                new Photo
                {
                    OwnerId = 4,
                    Path = "example4.jpg",
                    FilterId = 2,
                    Description = "Fourth example",
                    Iso = 50000, Aperture = 12,
                    Exposure = 0.00625,
                    FocalLength = 20
                },
                new Photo
                {
                    OwnerId = 5,
                    Path = "example5.jpg",
                    FilterId = 9,
                    Description = "Fifth example",
                    Iso = 6000, Aperture = 16,
                    Exposure = 0.00625,
                    FocalLength = 21
                },
                new Photo
                {
                    OwnerId = 6,
                    Path = "example1.jpg",
                    FilterId = 11,
                    Description = "Sixth example",
                    Iso = 70000, Aperture = 12,
                    Exposure = 0.00625,
                    FocalLength = 14
                }
            };
            public static List<Tag> Tags = new List<Tag>
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
        }
    }
}
