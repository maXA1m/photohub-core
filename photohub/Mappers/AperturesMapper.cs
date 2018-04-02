using System.Collections.Generic;
#region using PhotoHub.BLL
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
#endregion
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Mappers
{
    public class AperturesMapper : IMapper<ApertureViewModel, ApertureDTO>
    {
        public ApertureViewModel Map(ApertureDTO item)
        {
            if (item == null)
                return null;

            return new ApertureViewModel()
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        public List<ApertureViewModel> MapRange(IEnumerable<ApertureDTO> items)
        {
            if (items == null)
                return null;

            List<ApertureViewModel> apertures = new List<ApertureViewModel>();
            foreach (ApertureDTO item in items)
            {
                apertures.Add(new ApertureViewModel()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return apertures;
        }
    }
}
