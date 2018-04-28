#region using System
using System;
using System.Collections.Generic;
#endregion

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
        public int? Iso { get; set; }
        public double? Exposure { get; set; }//sec
        public double? Aperture { get; set; }//f/
        public double? FocalLength { get; set; }// in mm

        public int OwnerId { get; set; }
        public User Owner { get; set; }

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