using System.Collections.Generic;
using PhotoHub.DAL.Entities;
#region using PhotoHub.BLL
using PhotoHub.BLL.DTO;
using PhotoHub.BLL.Interfaces;
#endregion

namespace PhotoHub.BLL.Mappers
{
    public class TagsMapper : IMapper<TagDTO, Tag>
    {
        public TagDTO Map(Tag item)
        {
            if (item == null)
                return null;

            return new TagDTO()
            {
                Id = item.Id,
                Name = item.Name
            };
        }

        public List<TagDTO> MapRange(IEnumerable<Tag> items)
        {
            if (items == null)
                return null;

            List<TagDTO> tags = new List<TagDTO>();
            foreach (Tag tag in items)
            {
                tags.Add(new TagDTO()
                {
                    Id = tag.Id,
                    Name = tag.Name
                });
            }
            return tags;
        }
    }
}
