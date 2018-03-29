using System.Collections.Generic;
using PhotoHub.DAL.Entities;
#region using PhotoHub.BLL
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
#endregion

namespace PhotoHub.BLL.Mappers
{
    public class ExposuresMapper : IMapper<ExposureDTO, Exposure>
    {
        public ExposureDTO Map(Exposure item)
        {
            return new ExposureDTO()
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        public List<ExposureDTO> MapRange(IEnumerable<Exposure> items)
        {
            List<ExposureDTO> exposures = new List<ExposureDTO>();
            foreach (Exposure item in items)
            {
                exposures.Add(new ExposureDTO()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return exposures;
        }
    }
}
