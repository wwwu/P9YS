using P9YS.Services;
using P9YS.Services.Movie.Dto;
using P9YS.Services.MovieComment.Dto;
using P9YS.Services.MovieQuestion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P9YS.Web.Models
{
    public class MovieViewModel
    {
        public MovieInfo_Output MovieInfo { get; set; }

        public List<MovieSeries_Output> MovieSeries { get; set; }

        public MovieOrigin_Output MovieOrigin { get; set; }

        public PagingOutput<MovieComment_Output> MovieComments { get; set; }
        public int MovieCommentsCount { get; set; }

        public PagingOutput<QuestionTitle_Output> QuestionTitles { get; set; }
    }
}
