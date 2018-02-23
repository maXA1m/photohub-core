using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoHub.WEB.Mappers
{
    public class FilterDTOMapper
    {
        public static List<FilterViewModel> ToFilterViewModels(IEnumerable<FilterDTO> filters)
        {
            List<FilterViewModel> filterViewModels = new List<FilterViewModel>();
            foreach (FilterDTO filter in filters)
            {
                filterViewModels.Add(new FilterViewModel()
                {
                    Id = filter.Id,
                    Name = filter.Name
                });
            }
            return filterViewModels;
        }
    }
}
