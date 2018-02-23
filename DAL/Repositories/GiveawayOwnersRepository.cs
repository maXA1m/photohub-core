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
    public class GiveawayOwnersRepository : IRepository<GiveawayOwner>
    {
        private readonly ApplicationDbContext _context;

        public GiveawayOwnersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<GiveawayOwner> GetAll(int page, int pageSize)
        {
            return _context.GiveawayOwners
                            .Include(c => c.Giveaway)
                            .Include(c => c.Owner)
                            .Skip(page * pageSize).Take(pageSize);
        }
        public async Task<IEnumerable<GiveawayOwner>> GetAllAsync(int page, int pageSize)
        {
            return await _context.GiveawayOwners
                            .Include(c => c.Giveaway)
                            .Include(c => c.Owner)
                            .Skip(page * pageSize).Take(pageSize).ToListAsync();
        }

        public GiveawayOwner Get(int id)
        {
            return _context.GiveawayOwners
                    .Include(c => c.Giveaway)
                    .Include(c => c.Owner)
                    .Where(c => c.Id == id).FirstOrDefault();
        }
        public async Task<GiveawayOwner> GetAsync(int id)
        {
            return await _context.GiveawayOwners
                            .Include(c => c.Giveaway)
                            .Include(c => c.Owner)
                            .Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public IEnumerable<GiveawayOwner> Find(Func<GiveawayOwner, bool> predicate)
        {
            return _context.GiveawayOwners.Where(predicate);
        }

        public void Create(GiveawayOwner item)
        {
            _context.GiveawayOwners.Add(item);
        }
        public async Task CreateAsync(GiveawayOwner item)
        {
            await _context.GiveawayOwners.AddAsync(item);
        }

        public void Update(GiveawayOwner item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            GiveawayOwner giveawayOwner = _context.GiveawayOwners.Find(id);
            if (giveawayOwner != null)
                _context.GiveawayOwners.Remove(giveawayOwner);
        }
        public async Task DeleteAsync(int id)
        {
            GiveawayOwner giveawayOwner = await _context.GiveawayOwners.FindAsync(id);
            if (giveawayOwner != null)
                _context.GiveawayOwners.Remove(giveawayOwner);
        }
    }
}
