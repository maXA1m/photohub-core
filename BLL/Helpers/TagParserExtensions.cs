using System.Linq;
using System.Collections.Generic;
using PhotoHub.DAL.Entities;

namespace PhotoHub.BLL.Helpers
{
    /// <summary>
    /// Extensions class with method for parsing strings with tag names. 
    /// </summary>
    public static class TagParserExtensions
    {
        /// <summary>
        /// Extension method for parsing tags from string to <see cref="Tag"/> array.
        /// </summary>
        public static IEnumerable<string> SplitTags(this string strings, IEnumerable<Tag> tags)
        {
            var response = new List<string>();

            foreach (var tagString in strings.Split(new char[] { ',', '.', '-', '_', ' ', ':', '/' }))
            {
                var tag = tags.Where(t => t.Name == tagString).FirstOrDefault();

                if(tag != null)
                {
                    response.Add(tag.Name);
                }

                if (response.Count > 5)
                {
                    break;
                }
            }

            return response;
        }
    }
}
