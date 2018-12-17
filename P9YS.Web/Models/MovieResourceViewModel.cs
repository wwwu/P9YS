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
        public MovieInfoOutput MovieInfo { get; set; }

        public List<MovieOnlinePlayOutput> MovieOnlinePlays { get; set; }

        public List<MovieResourceOutput> MovieResources { get; set; }
    }
}
