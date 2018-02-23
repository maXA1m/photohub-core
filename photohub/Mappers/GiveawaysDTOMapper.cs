using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoHub.WEB.Mappers
{
    public class GiveawaysDTOMapper
    {
        public static GiveawayViewModel ToGiveawayViewModel(GiveawayDTO giveaway)
        {
            return new GiveawayViewModel()
            {
                Id = giveaway.Id,
                Name = giveaway.Name,
                Email = giveaway.Email,
                Avatar = giveaway.Avatar,
                About = giveaway.About,
                DateStart = giveaway.DateStart.ToString(),
                DateEnd = giveaway.DateEnd.ToString()
            };
        }

        public static List<GiveawayViewModel> ToGiveawayViewModels(IEnumerable<GiveawayDTO> giveaways)
        {
            List<GiveawayViewModel> giveawayViewModels = new List<GiveawayViewModel>();

            foreach (GiveawayDTO giveaway in giveaways)
            {
                giveawayViewModels.Add(new GiveawayViewModel()
                {
                    Id = giveaway.Id,
                    Name = giveaway.Name,
                    Email = giveaway.Email,
                    Avatar = giveaway.Avatar,
                    About = giveaway.About,
                    DateStart = giveaway.DateStart.ToString(),
                    DateEnd = giveaway.DateEnd.ToString()
                });
            }

            return giveawayViewModels;
        }

        public static GiveawayDetailsViewModel ToGiveawayDetailsViewModel(GiveawayDetailsDTO giveaway)
        {
            return new GiveawayDetailsViewModel()
            {
                Id = giveaway.Id,
                Name = giveaway.Name,
                Email = giveaway.Email,
                Avatar = giveaway.Avatar,
                About = giveaway.About,
                DateStart = giveaway.DateStart.ToString(),
                DateEnd = giveaway.DateEnd.ToString(),
                Winners = UserDTOMapper.ToUserViewModels(giveaway.Winners),
                Participants = UserDTOMapper.ToUserViewModels(giveaway.Participants),
                Owners = UserDTOMapper.ToUserViewModels(giveaway.Owners)
            };
        }

        public static List<GiveawayDetailsViewModel> ToGiveawayDetailsViewModels(IEnumerable<GiveawayDetailsDTO> giveaways)
        {
            List<GiveawayDetailsViewModel> giveawayDetailsViewModels = new List<GiveawayDetailsViewModel>();

            foreach (GiveawayDetailsDTO giveaway in giveaways)
            {
                giveawayDetailsViewModels.Add(new GiveawayDetailsViewModel()
                {
                    Id = giveaway.Id,
                    Name = giveaway.Name,
                    Email = giveaway.Email,
                    Avatar = giveaway.Avatar,
                    About = giveaway.About,
                    DateStart = giveaway.DateStart.ToString(),
                    DateEnd = giveaway.DateEnd.ToString(),
                    Winners = UserDTOMapper.ToUserViewModels(giveaway.Winners),
                    Participants = UserDTOMapper.ToUserViewModels(giveaway.Participants),
                    Owners = UserDTOMapper.ToUserViewModels(giveaway.Owners)
                });
            }

            return giveawayDetailsViewModels;
        }
    }
}
