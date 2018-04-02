using PhotoHub.DAL.Entities;
using System.Collections.Generic;

namespace PhotoHub.BLL.Interfaces
{
    public interface ITagParser
    {
        IEnumerable<string> Parse(string tagsString, IEnumerable<Tag> tags);
    }
}
