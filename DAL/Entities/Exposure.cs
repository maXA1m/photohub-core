using System.Collections.Generic;

namespace PhotoHub.DAL.Entities
{
    public class Exposure : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}