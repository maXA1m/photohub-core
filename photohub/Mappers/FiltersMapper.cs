using System.Collections.Generic;
using PhotoHub.BLL.DTO;
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Mappers
{
    /// <summary>
    /// Methods for mapping filter DTOs to filter view models.
    /// </summary>
    public static class FiltersMapper
    {
        #region Logic

        /// <summary>
        /// Maps filter DTO to filter view model.
        /// </summary>
        public static FilterViewModel Map(FilterDTO item)
        {
            if (item == null)
            {
                return null;
            }

            return new FilterViewModel
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        /// <summary>
        /// Maps filter DTOs to filter view models.
        /// </summary>
        public static List<FilterViewModel> MapRange(IEnumerable<FilterDTO> items)
        {
            if (items == null)
            {
                return null;
            }

            var filters = new List<FilterViewModel>();

            foreach (var item in items)
            {
                filters.Add(new FilterViewModel
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }

            return filters;
        }

        #endregion
    }
}
