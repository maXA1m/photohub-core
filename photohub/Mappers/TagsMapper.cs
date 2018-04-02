using System.Collections.Generic;
#region using PhotoHub.BLL
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
#endregion
using PhotoHub.WEB.ViewModels;

namespace PhotoHub.WEB.Mappers
{
    public class TagsMapper : IMapper<TagViewModel, TagDTO>
    {
        public TagViewModel Map(TagDTO item)
        {
            if (item == null)
                return null;

            return new TagViewModel()
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        public List<TagViewModel> MapRange(IEnumerable<TagDTO> items)
        {
            if (items == null)
                return null;

            List<TagViewModel> tags = new List<TagViewModel>();
            foreach (TagDTO item in items)
            {
                tags.Add(new TagViewModel()
                {
                    Id = item.Id,
                    Name = item.Name
                });
            }
            return tags;
        }
    }
}
