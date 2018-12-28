using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services
{
    public class PagingInput
    {
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }

    public class PagingInput<T> : PagingInput
    {
        public T Condition { get; set; }
    }
}
