using System.Collections.Generic;
using PhotoHub.DAL.Entities;
#region using PhotoHub.BLL
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
#endregion

namespace PhotoHub.BLL.Mappers
{
    public class FiltersMapper : IMapper<FilterDTO, Filter>
    {
        public FilterDTO Map(Filter item)
        {
            return new FilterDTO()
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        public List<FilterDTO> MapRange(IEnumerable<Filter> items)
        {
            List<FilterDTO> filters = new List<FilterDTO>();
            foreach (Filter filter in items)
            {
                filters.Add(new FilterDTO()
                {
                    Id = filter.Id,
                    Name = filter.Name
                });
            }
            return filters;
        }
    }
}
