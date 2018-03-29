using System.Collections.Generic;

namespace PhotoHub.DAL.Entities
{
    public class Aperture : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
