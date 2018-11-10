using System;

namespace PhotoHub.BLL.DTO
{
    /// <summary>
    /// Comment data transfer object.
    /// Contains <see cref="Id"/>, <see cref="Text"/>, <see cref="Date"/>, <see cref="Owner"/> DTO.
    /// </summary>
    public class CommentDTO
    {
        /// <summary>
        /// Gets and sets comment id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets and sets comment text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets and sets comment date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets and sets comment owner DTO.
        /// </summary>
        public UserDTO Owner { get; set; }
    }
}