using System;
using System.Collections.Generic;

namespace PhotoHub.DAL.Entities
{
    public class Photo : BaseEntity
    {
        public string Path { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int CountViews { get; set; }

        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Iso { get; set; }
        public string Exposure { get; set; }
        public string Aperture { get; set; }
        public string FocalLength { get; set; }

        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        public int FilterId { get; set; }
        public Filter Filter { get; set; }
        
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Photo()
        {
            FilterId = 1;
            Date = DateTime.Now;
            CountViews = 0;
        }
    }
}