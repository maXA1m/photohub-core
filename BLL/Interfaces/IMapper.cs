using System.Collections.Generic;

namespace PhotoHub.BLL.Interfaces
{
    public interface IMapper<N, T> where T : class
    {
        N Map(T item);
        List<N> MapRange(IEnumerable<T> items);
    }
}
