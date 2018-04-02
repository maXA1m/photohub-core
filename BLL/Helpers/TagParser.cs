#region using System
using System.Linq;
using System.Collections.Generic;
#endregion
using PhotoHub.BLL.Interfaces;
using PhotoHub.DAL.Entities;

namespace PhotoHub.BLL.Helpers
{
    public class TagParser : ITagParser
    {
        public IEnumerable<string> Parse(string tagsString, IEnumerable<Tag> tags)
        {
            List<string> response = new List<string>();
            foreach (string tag in tagsString.Split(new char[] { ',', '.', '-', '_', ' ', ':', '/' }))
            {
                Tag tg = tags.Where(t => t.Name == tag).FirstOrDefault();
                if(tg != null)
                    response.Add(tg.Name);

                if (response.Count > 5)
                    break;
            }

            return response;
        }
    }
}
