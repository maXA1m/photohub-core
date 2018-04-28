#region using System/Microsoft
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
#endregion
#region using PhotoHub.DAL
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Data;
using PhotoHub.DAL.Entities;
#endregion

namespace PhotoHub.DAL.Repositories
{
    public class UsersRepository : IRepository<User>
    {
        private readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.AppUsers;
        }

        public IEnumerable<User> GetAll(int page, int pageSize)
        {
            return _context.AppUsers.Skip(page * pageSize).Take(pageSize);
        }

        public User Get(int id)
        {
            return _context.AppUsers.Find(id);
        }
        public async Task<User> GetAsync(int id)
        {
            return await _context.AppUsers.FindAsync(id);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return _context.AppUsers.Where(predicate);
        }

        public void Create(User item)
        {
            _context.AppUsers.Add(item);
        }
        public async Task CreateAsync(User item)
        {
            await _context.AppUsers.AddAsync(item);
        }

        public void Update(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User user = _context.AppUsers.Find(id);
            if (user != null)
                _context.AppUsers.Remove(user);
        }
        public async Task DeleteAsync(int id)
        {
            User user = await _context.AppUsers.FindAsync(id);
            if (user != null)
                _context.AppUsers.Remove(user);
        }
    }
}