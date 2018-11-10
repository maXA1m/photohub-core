namespace PhotoHub.BLL.DTO
{
    /// <summary>
    /// Filter data transfer object.
    /// Contains <see cref="Id"/> and <see cref="Name"/> of filter.
    /// </summary>
    public class FilterDTO
    {
        /// <summary>
        /// Gets and sets filter id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets and sets filter name.
        /// </summary>
        public string Name { get; set; }
    }
}