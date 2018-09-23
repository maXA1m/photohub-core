using System.Linq;
using System.Collections.Generic;
using PhotoHub.DAL.Entities;

namespace PhotoHub.BLL.Helpers
{
    /// <summary>
    /// Static class with method for parsing strings with tag names. 
    /// </summary>
    public static class TagParser
    {
        #region Logic

        /// <summary>
        /// Static method for parsing tags from string to <see cref="Tag"/> array.
        /// </summary>
        public static IEnumerable<string> Parse(string tagsString, IEnumerable<Tag> tags)
        {
            var response = new List<string>();

            foreach (var tag in tagsString.Split(new char[] { ',', '.', '-', '_', ' ', ':', '/' }))
            {
                Tag tg = tags.Where(t => t.Name == tag).FirstOrDefault();

                if(tg != null)
                {
                    response.Add(tg.Name);
                }

                if (response.Count > 5)
                {
                    break;
                }
            }

            return response;
        }

        #endregion
    }
}
