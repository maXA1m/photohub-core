﻿#region using System/Microsoft
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
    public class BlockingsRepository : IRepository<BlackList>
    {
        private readonly ApplicationDbContext _context;

        public BlockingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<BlackList> GetAll()
        {
            return _context.BlackLists
                            .Include(c => c.BlockedUser)
                            .Include(c => c.User);
        }

        public IEnumerable<BlackList> GetAll(int page, int pageSize)
        {
            return _context.BlackLists
                            .Include(c => c.BlockedUser)
                            .Include(c => c.User)
                            .Skip(page * pageSize).Take(pageSize);
        }

        public BlackList Get(int id)
        {
            return _context.BlackLists
                    .Include(c => c.BlockedUser)
                    .Include(c => c.User)
                    .Where(c => c.Id == id).FirstOrDefault();
        }
        public async Task<BlackList> GetAsync(int id)
        {
            return await _context.BlackLists
                            .Include(c => c.BlockedUser)
                            .Include(c => c.User)
                            .Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public IEnumerable<BlackList> Find(Func<BlackList, bool> predicate)
        {
            return _context.BlackLists
                    .Include(c => c.BlockedUser)
                    .Include(c => c.User)
                    .Where(predicate);
        }

        public void Create(BlackList item)
        {
            _context.BlackLists.Add(item);
        }
        public async Task CreateAsync(BlackList item)
        {
            await _context.BlackLists.AddAsync(item);
        }

        public void Update(BlackList item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            BlackList item = _context.BlackLists.Find(id);
            if (item != null)
                _context.BlackLists.Remove(item);
        }
        public async Task DeleteAsync(int id)
        {
            BlackList item = await _context.BlackLists.FindAsync(id);
            if (item != null)
                _context.BlackLists.Remove(item);
        }
    }
}