using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.Movie.Dto
{
    public class MovieListOutput
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public decimal Score { get; set; }
        public string ImgUrl { get; set; }
    }
}
