using System.Collections.Generic;
#region using PhotoHub.BLL
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
#endregion
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Mappers
{
    public class FiltersMapper : IMapper<FilterViewModel, FilterDTO>
    {
        public FilterViewModel Map(FilterDTO item)
        {
            return new FilterViewModel()
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        public List<FilterViewModel> MapRange(IEnumerable<FilterDTO> items)
        {
            List<FilterViewModel> filters = new List<FilterViewModel>();
            foreach (FilterDTO item in items)
            {
                filters.Add(new FilterViewModel()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return filters;
        }
    }
}
