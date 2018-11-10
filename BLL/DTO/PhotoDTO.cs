using System;
using System.Collections.Generic;

namespace PhotoHub.BLL.DTO
{
    /// <summary>
    /// Photo data transfer object.
    /// Contains photo properties, <see cref="Owner"/> DTO, likes DTO collection, comments DTO collection and tags DTO collection.
    /// </summary>
    public class PhotoDTO
    {
        /// <summary>
        /// Gets and sets photo id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets and sets path to photo.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets and sets photo filter name.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Gets and sets photo description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets and sets photo count of views.
        /// </summary>
        public int CountViews { get; set; }

        /// <summary>
        /// Gets and sets photo date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets and sets photo owner DTO.
        /// </summary>
        public UserDTO Owner { get; set; }

        /// <summary>
        /// Gets and sets photo liked state by current user.
        /// </summary>
        public bool Liked { get; set; }

        /// <summary>
        /// Gets and sets photo bookmarked state by current user.
        /// </summary>
        public bool Bookmarked { get; set; }

        /// <summary>
        /// Gets and sets photo camera Manufacturer.
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Gets and sets photo camera Model.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Gets and sets photo camera Iso.
        /// </summary>
        public int? Iso { get; set; }

        /// <summary>
        /// Gets and sets photo camera Exposure.
        /// </summary>
        public double? Exposure { get; set; }

        /// <summary>
        /// Gets and sets photo camera Aperture.
        /// </summary>
        public double? Aperture { get; set; }

        /// <summary>
        /// Gets and sets photo camera Focal Length.
        /// </summary>
        public double? FocalLength { get; set; }

        /// <summary>
        /// Gets and sets collection of photo likes.
        /// </summary>
        public IEnumerable<LikeDTO> Likes { get; set; }

        /// <summary>
        /// Gets and sets collection of photo comments.
        /// </summary>
        public IEnumerable<CommentDTO> Comments { get; set; }

        /// <summary>
        /// Gets and sets collection of photo tags.
        /// </summary>
        public IEnumerable<TagDTO> Tags { get; set; }
    }
}