using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services
{
    public class PagingOutput<T> where T : class
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public List<T> Data { get; set; }
    }
}
