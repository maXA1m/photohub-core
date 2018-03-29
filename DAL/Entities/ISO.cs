using System.Collections.Generic;

namespace PhotoHub.DAL.Entities
{
    public class ISO : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
