using PhotoHub.BLL.DTO;
using PhotoHub.DAL.Entities;
using System.Collections.Generic;

namespace PhotoHub.BLL.Mappers
{
    public class FilterMapper
    {
        public static List<FilterDTO> ToFilterDTOs(IEnumerable<Filter> filters)
        {
            List<FilterDTO> filterDTOs = new List<FilterDTO>();
            foreach(Filter filter in filters)
            {
                filterDTOs.Add(new FilterDTO()
                {
                    Id = filter.Id,
                    Name = filter.Name
                });
            }
            return filterDTOs;
        }
    }
}
