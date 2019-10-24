using System;
using System.Linq;
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
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContextSeeder"/>.
        /// </summary>
        public ApplicationDbContextSeeder(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Seeds database with default values of filters, photos, tags and users (identity too).
        /// </summary>
        public async Task Seed()
        {
            if (_context.Photos.Any())
            {
                return;
            }

            await _context.AddRangeAsync(Defaults.Entities.Filters);
            await _context.AddRangeAsync(Defaults.Entities.Tags);
            Defaults.Entities.Identities.ForEach(i => _userManager.CreateAsync(i, Defaults.Strings.IdentitiesPassword).Wait());
            await _context.AddRangeAsync(Defaults.Entities.Users);
            await _context.AddRangeAsync(Defaults.Entities.Photos);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Creates user roles and first admin.
        /// </summary>
        public async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var isAdminExists = await RoleManager.RoleExistsAsync(Defaults.Strings.AdminRole);
            if (isAdminExists)
            {
                return;
            }

            var user = await UserManager.FindByNameAsync(Defaults.Strings.AdminUserName);

            await RoleManager.CreateAsync(new IdentityRole(Defaults.Strings.AdminRole));
            await UserManager.AddToRoleAsync(user, Defaults.Strings.AdminRole);
        }
    }
}
