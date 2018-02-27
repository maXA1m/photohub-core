using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using PhotoHub.DAL.Interfaces;
using PhotoHub.DAL.Entities;
using PhotoHub.BLL.Interfaces;
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Mappers;

namespace PhotoHub.BLL.Services
{
    public class GiveawaysService : IGiveawaysService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ApplicationUser CurrentUser => _unitOfWork.Users.Find(u => u.UserName == _httpContextAccessor.HttpContext.User.Identity.Name).FirstOrDefault();
        public UserDTO CurrentUserDTO
        {
            get
            {
                ApplicationUser user = CurrentUser;

                return UserMapper.ToUserDTO(
                    user,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.Id && f.UserId == user.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == user.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.Id && b.UserId == user.Id).FirstOrDefault() != null
                );
            }
        }

        public GiveawaysService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<GiveawayDTO> GetAll(int page, int pageSize)
        {
            IEnumerable<Giveaway> giveaways = _unitOfWork.Giveaways.GetAll(page, pageSize);

            List<GiveawayDTO> giveawayDTOs = new List<GiveawayDTO>(pageSize);

            foreach (Giveaway giveaway in giveaways)
                GiveawayMapper.ToGiveawayDTO(giveaway);

            return giveawayDTOs;
        }
        public async Task<IEnumerable<GiveawayDTO>> GetAllAsync(int page, int pageSize)
        {
            IEnumerable<Giveaway> giveaways = await _unitOfWork.Giveaways.GetAllAsync(page, pageSize);

            List<GiveawayDTO> giveawayDTOs = new List<GiveawayDTO>(pageSize);

            foreach (Giveaway giveaway in giveaways)
                GiveawayMapper.ToGiveawayDTO(giveaway);

            return giveawayDTOs;
        }

        public GiveawayDetailsDTO Get(int id)
        {
            ApplicationUser currentUser = CurrentUser;
            Giveaway giveaway = _unitOfWork.Giveaways.Get(id);

            List<UserDTO> winners = new List<UserDTO>();
            foreach (Winner user in giveaway.Winners)
            {
                winners.Add(UserMapper.ToUserDTO(
                    user.User,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.UserId).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.UserId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.UserId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                ));
            }

            List<UserDTO> owners = new List<UserDTO>();
            foreach (GiveawayOwner user in giveaway.Owners)
            {
                owners.Add(UserMapper.ToUserDTO(
                    user.Owner,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.OwnerId).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                ));
            }

            List<UserDTO> participants = new List<UserDTO>();
            foreach (Participant user in giveaway.Participants)
            {
                participants.Add(UserMapper.ToUserDTO(
                    user.User,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.UserId).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.UserId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.UserId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                ));
            }

            return GiveawayMapper.ToGiveawayDetailsDTO(giveaway, winners, participants, owners);
        }
        public async Task<GiveawayDetailsDTO> GetAsync(int id)
        {
            ApplicationUser currentUser = CurrentUser;
            Giveaway giveaway =  await _unitOfWork.Giveaways.GetAsync(id);

            List<UserDTO> winners = new List<UserDTO>();
            foreach (Winner user in giveaway.Winners)
            {
                winners.Add(UserMapper.ToUserDTO(
                    user.User,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.UserId).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.UserId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.UserId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                ));
            }

            List<UserDTO> owners = new List<UserDTO>();
            foreach (GiveawayOwner user in giveaway.Owners)
            {
                owners.Add(UserMapper.ToUserDTO(
                    user.Owner,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.OwnerId).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.OwnerId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.OwnerId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                ));
            }

            List<UserDTO> participants = new List<UserDTO>();
            foreach (Participant user in giveaway.Participants)
            {
                participants.Add(UserMapper.ToUserDTO(
                    user.User,
                    _unitOfWork.Confirmations.Find(c => c.UserId == user.UserId).FirstOrDefault() != null,
                    _unitOfWork.Followings.Find(f => f.FollowedUserId == user.UserId && f.UserId == currentUser.Id).FirstOrDefault() != null,
                    _unitOfWork.Blockings.Find(b => b.BlockedUserId == user.UserId && b.UserId == currentUser.Id).FirstOrDefault() != null,
                            _unitOfWork.Blockings.Find(b => b.BlockedUserId == currentUser.Id && b.UserId == currentUser.Id).FirstOrDefault() != null
                ));
            }

            return GiveawayMapper.ToGiveawayDetailsDTO(giveaway, winners, participants, owners);
        }

        public IEnumerable<GiveawayDTO> GetForUser(int page, string userName, int pageSize)
        {
            ApplicationUser user = _unitOfWork.Users.Find(u => u.UserName == userName).FirstOrDefault();

            if (user != null)
            {
                IEnumerable<GiveawayOwner> giveawayOwners = _unitOfWork.GiveawayOwners.Find(go => go.OwnerId == user.Id);

                List<GiveawayDTO> giveaways = new List<GiveawayDTO>();
                foreach (GiveawayOwner giveawayOwner in giveawayOwners)
                {
                    foreach (Giveaway giveaway in _unitOfWork.Giveaways.Find(g => g.Id == giveawayOwner.GiveawayId))
                        giveaways.Add(GiveawayMapper.ToGiveawayDTO(giveaway));
                }

                return giveaways.Skip(page * pageSize).Take(pageSize);
            }

            return null;
        }

        public int Create(string name, string email, string about, string avatar)
        {
            ApplicationUser currentUser = CurrentUser;
            Giveaway giveaway = new Giveaway()
            {
                Name = name,
                Email = email,
                About = about,
                Avatar = avatar
            };

            if (String.IsNullOrEmpty(giveaway.Email))
                giveaway.Email = currentUser.Email;

            _unitOfWork.Giveaways.Create(giveaway);
            _unitOfWork.Save();

            _unitOfWork.GiveawayOwners.Create(new GiveawayOwner()
            {
                GiveawayId = giveaway.Id,
                OwnerId = currentUser.Id
            });
            _unitOfWork.Save();

            return giveaway.Id;
        }
        public async Task<int> CreateAsync(string name, string email, string about, string avatar)
        {
            ApplicationUser currentUser = CurrentUser;
            Giveaway giveaway = new Giveaway()
            {
                Name = name,
                Email = email,
                About = about,
                Avatar = avatar
            };

            if (String.IsNullOrEmpty(giveaway.Email))
                giveaway.Email = currentUser.Email;

            await _unitOfWork.Giveaways.CreateAsync(giveaway);
            await _unitOfWork.SaveAsync();

            await _unitOfWork.GiveawayOwners.CreateAsync(new GiveawayOwner()
            {
                GiveawayId = giveaway.Id,
                OwnerId = currentUser.Id
            });
            await _unitOfWork.SaveAsync();

            return giveaway.Id;
        }

        public void Edit(int id, string name, string email, string about, string avatar)
        {
            Giveaway giveaway = _unitOfWork.Giveaways.Get(id);
            if (giveaway != null)
            {
                if (name != null)
                    giveaway.Name = name;
                if (email != null)
                    giveaway.Email = email;
                if (about != null)
                    giveaway.About = about;
                if (avatar != null)
                    giveaway.Avatar = avatar;

                _unitOfWork.Giveaways.Update(giveaway);
                _unitOfWork.Save();
            }
        }
        public async Task EditAsync(int id, string name, string email, string about, string avatar)
        {
            Giveaway giveaway = await _unitOfWork.Giveaways.GetAsync(id);
            if(giveaway != null)
            {
                if (name != null)
                    giveaway.Name = name;
                if (email != null)
                    giveaway.Email = email;
                if (about != null)
                    giveaway.About = about;
                if (avatar != null)
                    giveaway.Avatar = avatar;

                _unitOfWork.Giveaways.Update(giveaway);
                await _unitOfWork.SaveAsync();
            }
        }

        public void Enter(int id)
        {
            ApplicationUser user = CurrentUser;
            Giveaway giveaway = _unitOfWork.Giveaways.Get(id);

            if (user != null && giveaway != null && _unitOfWork.Participants.Find(p => p.UserId == user.Id && p.GiveawayId == id).FirstOrDefault() == null)
            {
                Participant participant = new Participant()
                {
                    UserId = user.Id,
                    GiveawayId = giveaway.Id
                };

                _unitOfWork.Participants.Create(participant);
                _unitOfWork.Save();
            }
        }
        public async Task EnterAsync(int id)
        {
            ApplicationUser user = CurrentUser;
            Giveaway giveaway = await _unitOfWork.Giveaways.GetAsync(id);

            if (user != null && giveaway != null && _unitOfWork.Participants.Find(p => p.UserId == user.Id && p.GiveawayId == id).FirstOrDefault() == null)
            {
                Participant participant = new Participant()
                {
                    UserId = user.Id,
                    GiveawayId = giveaway.Id
                };

                await _unitOfWork.Participants.CreateAsync(participant);
                await _unitOfWork.SaveAsync();
            }
        }

        public void Leave(int id)
        {
            ApplicationUser user = CurrentUser;
            Giveaway giveaway = _unitOfWork.Giveaways.Get(id);
            Participant participant = _unitOfWork.Participants.Find(p => p.UserId == user.Id && p.GiveawayId == id).FirstOrDefault();

            if (user != null && giveaway != null && participant != null)
            {
                _unitOfWork.Participants.Delete(participant.Id);
                _unitOfWork.Save();
            }
        }
        public async Task LeaveAsync(int id)
        {
            ApplicationUser user = CurrentUser;
            Giveaway giveaway = await _unitOfWork.Giveaways.GetAsync(id);
            Participant participant = _unitOfWork.Participants.Find(p => p.UserId == user.Id && p.GiveawayId == id).FirstOrDefault();

            if (user != null && giveaway != null && participant != null)
            {
                await _unitOfWork.Participants.DeleteAsync(participant.Id);
                await _unitOfWork.SaveAsync();
            }
        }

        public void Delete(int id)
        {
            _unitOfWork.Giveaways.Delete(id);
            _unitOfWork.Save();
        }
        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Giveaways.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}