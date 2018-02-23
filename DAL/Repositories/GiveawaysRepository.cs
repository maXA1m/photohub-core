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
    public class GiveawaysRepository : IRepository<Giveaway>
    {
        private readonly ApplicationDbContext _context;

        public GiveawaysRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Giveaway> GetAll(int page, int pageSize)
        {
            return _context.Giveaways.Skip(page * pageSize).Take(pageSize);
        }
        public async Task<IEnumerable<Giveaway>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Giveaways.Skip(page * pageSize).Take(pageSize).ToListAsync();
        }

        public Giveaway Get(int id)
        {
            return _context.Giveaways
                    .Include(g => g.Owners)
                        .ThenInclude(go => go.Owner)
                    .Include(g => g.Participants)
                        .ThenInclude(p => p.User)
                    .Include(g => g.Winners)
                        .ThenInclude(w => w.User)
                    .OrderBy(g => g.DateStart)
                    .Where(g => g.Id == id).FirstOrDefault();
        }
        public async Task<Giveaway> GetAsync(int id)
        {
            return await _context.Giveaways
                            .Include(g => g.Owners)
                                .ThenInclude(go => go.Owner)
                            .Include(g => g.Participants)
                                .ThenInclude(p => p.User)
                            .Include(g => g.Winners)
                                .ThenInclude(w => w.User)
                            .OrderBy(g => g.DateStart)
                            .Where(g => g.Id == id).FirstOrDefaultAsync();
        }

        public IEnumerable<Giveaway> Find(Func<Giveaway, bool> predicate)
        {
            return _context.Giveaways.Where(predicate);
        }

        public void Create(Giveaway item)
        {
            _context.Giveaways.Add(item);
        }
        public async Task CreateAsync(Giveaway item)
        {
            await _context.Giveaways.AddAsync(item);
        }

        public void Update(Giveaway item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Giveaway giveaway = _context.Giveaways.Find(id);
            if (giveaway != null)
                _context.Giveaways.Remove(giveaway);
        }
        public async Task DeleteAsync(int id)
        {
            Giveaway giveaway = await _context.Giveaways.FindAsync(id);
            if (giveaway != null)
                _context.Giveaways.Remove(giveaway);
        }
    }
}