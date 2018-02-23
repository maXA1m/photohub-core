using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhotoHub.DAL.Data;

namespace PhotoHub.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Avatar { get; set; }
        public string About { get; set; }
        public DateTime Date { get; set; }

        public ApplicationUser() : base()
        {

        }
        
        public bool HasGiveaways(ApplicationDbContext context)
        {
            int count = (from g in context.GiveawayOwners
                         where g.OwnerId == this.Id
                         select g).Count();

            return count > 0 ? true : false;
        }
        public async Task<int> CountGiveaways(ApplicationDbContext context)
        {
            int count = (from g in await context.GiveawayOwners.ToListAsync()
                         where g.OwnerId == this.Id
                         select g).Count();

            return count;
        }
        public async Task<int> CountFollowers(ApplicationDbContext context)
        {
            var followers = (from follow in await context.Followings.ToListAsync()
                             where follow.FollowedUserId == this.Id
                             select follow);

            return followers.Count();
        }
        public async Task<int> CountFollowings(ApplicationDbContext context)
        {
            var followings = (from follow in await context.Followings.ToListAsync()
                              where follow.UserId == this.Id
                              select follow);
            
            return followings.Count();
        }
        public bool IsOwner(Giveaway giveaway, ApplicationDbContext context)
        {
            foreach (var go in context.GiveawayOwners)
            {
                if (go.OwnerId == Id && go.GiveawayId == giveaway.Id)
                    return true;
            }

            return false;
        }
        public bool IsParticipant(Giveaway giveaway, ApplicationDbContext context)
        {
            foreach (var p in context.Participants)
            {
                if (p.UserId == Id && p.GiveawayId == giveaway.Id)
                    return true;
            }

            return false;
        }
        public bool IsConfirmed(ApplicationDbContext context)
        {
            foreach (var c in context.Confirmed)
            {
                if (c.UserId == this.Id)
                    return true;
            }

            return false;
        }
        public bool IsBlocked(ApplicationUser blocked, ApplicationDbContext context)
        {
            foreach (var block in context.BlackLists)
            {
                if (block.UserId == blocked.Id && block.BlockedUserId == this.Id)
                    return true;
            }
            return false;
        }
        public bool IsBlockedBy(ApplicationUser blocker, ApplicationDbContext context)
        {
            foreach (var block in context.BlackLists)
            {
                if (block.UserId == Id && block.BlockedUserId == blocker.Id)
                    return true;
            }

            return false;
        }
    }
}
