using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoHub.BLL.Interfaces
{
    public interface IMapper<N, T> where T : class
    {
        N Map(T item);
        List<N> MapRange(IEnumerable<T> items);
    }
}
