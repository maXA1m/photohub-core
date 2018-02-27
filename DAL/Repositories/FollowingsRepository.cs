using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Data;
using PhotoHub.DAL.Entities;

namespace PhotoHub.DAL.Repositories
{
    public class FollowingsRepository : IRepository<Following>
    {
        private readonly ApplicationDbContext _context;

        public FollowingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Following> GetAll(int page, int pageSize)
        {
            return _context.Followings
                            .Include(c => c.FollowedUser)
                            .Include(c => c.User)
                            .Skip(page * pageSize).Take(pageSize);
        }
        public async Task<IEnumerable<Following>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Followings
                            .Include(c => c.FollowedUser)
                            .Include(c => c.User)
                            .Skip(page * pageSize).Take(pageSize).ToListAsync();
        }

        public Following Get(int id)
        {
            return _context.Followings
                    .Include(c => c.FollowedUser)
                    .Include(c => c.User)
                    .Where(c => c.Id == id).FirstOrDefault();
        }
        public async Task<Following> GetAsync(int id)
        {
            return await _context.Followings
                            .Include(c => c.FollowedUser)
                            .Include(c => c.User)
                            .Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public IEnumerable<Following> Find(Func<Following, bool> predicate)
        {
            return _context.Followings
                            .Include(c => c.FollowedUser)
                            .Include(c => c.User).Where(predicate);
        }
        public void Create(Following item)
        {
            _context.Followings.Add(item);
        }

        public async Task CreateAsync(Following item)
        {
            await _context.Followings.AddAsync(item);
        }
        public void Update(Following item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Following following = _context.Followings.Find(id);
            if (following != null)
                _context.Followings.Remove(following);
        }
        public async Task DeleteAsync(int id)
        {
            Following following = await _context.Followings.FindAsync(id);
            if (following != null)
                _context.Followings.Remove(following);
        }
    }
}