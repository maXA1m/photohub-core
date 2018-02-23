using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Data;
using PhotoHub.DAL.Entities;

namespace PhotoHub.DAL.Repositories
{
    public class ParticipantsRepository : IRepository<Participant>
    {
        private readonly ApplicationDbContext _context;

        public ParticipantsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Participant> GetAll(int page, int pageSize)
        {
            return _context.Participants.Include(c => c.User).Include(c => c.Giveaway).Skip(page * pageSize).Take(pageSize);
        }
        public async Task<IEnumerable<Participant>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Participants.Include(c => c.User).Include(c => c.Giveaway).Skip(page * pageSize).Take(pageSize).ToListAsync();
        }

        public IEnumerable<Participant> Find(Func<Participant, bool> predicate)
        {
            return _context.Participants.Include(c => c.User).Include(c => c.Giveaway).Where(predicate);
        }

        public Participant Get(int id)
        {
            return _context.Participants.Include(c => c.User).Include(c => c.Giveaway).Where(c => c.Id == id).FirstOrDefault();
        }
        public async Task<Participant> GetAsync(int id)
        {
            return await _context.Participants.Include(c => c.User).Include(c => c.Giveaway).Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public void Create(Participant item)
        {
            _context.Participants.Add(item);
        }
        public async Task CreateAsync(Participant item)
        {
            await _context.Participants.AddAsync(item);
        }

        public void Update(Participant item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Participant participant = _context.Participants.Find(id);
            if (participant != null)
                _context.Participants.Remove(participant);
        }
        public async Task DeleteAsync(int id)
        {
            Participant participant = await _context.Participants.FindAsync(id);
            if (participant != null)
                _context.Participants.Remove(participant);
        }
    }
}
