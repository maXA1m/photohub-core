using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

using PhotoHub.DAL.Data;
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Entities;

namespace PhotoHub.DAL.Repositories
{
    public class UsersRepository : IRepository<ApplicationUser>
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public IEnumerable<ApplicationUser> GetAll(int page, int pageSize)
        {
            return _context.Users.Skip(page * pageSize).Take(pageSize);
        }

        public ApplicationUser Get(int id)
        {
            return _context.Users.Find(id);
        }
        public async Task<ApplicationUser> GetAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public IEnumerable<ApplicationUser> Find(Func<ApplicationUser, bool> predicate)
        {
            return _context.Users.Where(predicate);
        }

        public void Create(ApplicationUser item)
        {
            _context.Users.Add(item);
        }
        public async Task CreateAsync(ApplicationUser item)
        {
            await _context.Users.AddAsync(item);
        }

        public void Update(ApplicationUser item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ApplicationUser user = _context.Users.Find(id);
            if (user != null)
                _context.Users.Remove(user);
        }
        public async Task DeleteAsync(int id)
        {
            ApplicationUser user = await _context.Users.FindAsync(id);
            if (user != null)
                _context.Users.Remove(user);
        }
    }
}