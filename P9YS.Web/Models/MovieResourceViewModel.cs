using P9YS.Services.Movie.Dto;
using P9YS.Services.MovieResource.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P9YS.Web.Models
{
    public class MovieResourceViewModel
    {
        public MovieInfo_Output MovieInfo { get; set; }

        public List<MovieOnlinePlay_Output> MovieOnlinePlays { get; set; }

        public List<MovieResource_Output> MovieResources { get; set; }
    }
}
