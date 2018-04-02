#region using System
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion
using PhotoHub.BLL.DTO;

namespace PhotoHub.BLL.Interfaces
{
    public interface ITagsService : IDisposable, IReturnUser
    {
        IEnumerable<TagDTO> GetAll();

        TagDTO Get(string name);
    }
}
