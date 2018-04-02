using System.Collections.Generic;
using PhotoHub.DAL.Entities;
#region using PhotoHub.BLL
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
#endregion

namespace PhotoHub.BLL.Mappers
{
    public class IsosMapper : IMapper<IsoDTO, ISO>
    {
        public IsoDTO Map(ISO item)
        {
            if (item == null)
                return null;

            return new IsoDTO()
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        public List<IsoDTO> MapRange(IEnumerable<ISO> items)
        {
            if (items == null)
                return null;

            List<IsoDTO> isos = new List<IsoDTO>();
            foreach (ISO item in items)
            {
                isos.Add(new IsoDTO()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return isos;
        }
    }
}
