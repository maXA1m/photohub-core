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
        public double FocalLength { get; set; }// in mm

        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        public int FilterId { get; set; }
        public Filter Filter { get; set; }
        public int IsoId { get; set; }
        public ISO Iso { get; set; }
        public int ExposureId { get; set; }
        public Exposure Exposure { get; set; }
        public int ApertureId { get; set; }
        public Aperture Aperture { get; set; }

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