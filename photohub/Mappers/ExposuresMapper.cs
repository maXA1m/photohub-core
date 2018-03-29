using System.Collections.Generic;
#region using PhotoHub.BLL
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
#endregion
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Mappers
{
    public class ExposuresMapper : IMapper<ExposureViewModel, ExposureDTO>
    {
        public ExposureViewModel Map(ExposureDTO item)
        {
            return new ExposureViewModel()
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        public List<ExposureViewModel> MapRange(IEnumerable<ExposureDTO> items)
        {
            List<ExposureViewModel> exposures = new List<ExposureViewModel>();
            foreach (ExposureDTO item in items)
            {
                exposures.Add(new ExposureViewModel()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return exposures;
        }
    }
}
