using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace API.Talabat.Helper
{
    public class Pagination<T>
    {
    

        public int PageSize { get; set; }
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public IEnumerable<T> Data { get; set; }
        public Pagination(int pageSize, int pageIndex, IEnumerable<T> data, int pageCount)
        {
            PageSize = pageSize;
            Count = pageCount;
            PageIndex = pageIndex;
            Data = data;
        }

    }
}
