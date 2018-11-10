using System;

namespace PhotoHub.BLL.DTO
{
    /// <summary>
    /// Like data transfer object.
    /// Contains <see cref="Id"/>, <see cref="Date"/> and <see cref="Owner"/> DTO.
    /// </summary>
    public class LikeDTO
    {
        /// <summary>
        /// Gets and sets like id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets and sets like date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets and sets like owner DTO.
        /// </summary>
        public UserDTO Owner { get; set; }
    }
}