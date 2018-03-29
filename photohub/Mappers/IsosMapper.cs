using System.Collections.Generic;
#region using PhotoHub.BLL
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
#endregion
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Mappers
{
    public class IsosMapper : IMapper<IsoViewModel, IsoDTO>
    {
        public IsoViewModel Map(IsoDTO item)
        {
            return new IsoViewModel()
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        public List<IsoViewModel> MapRange(IEnumerable<IsoDTO> items)
        {
            List<IsoViewModel> isos = new List<IsoViewModel>();
            foreach (IsoDTO item in items)
            {
                isos.Add(new IsoViewModel()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return isos;
        }
    }
}
