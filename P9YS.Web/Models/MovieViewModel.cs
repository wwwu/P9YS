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
        public MovieInfoOutput MovieInfo { get; set; }

        public List<MovieSeriesOutput> MovieSeries { get; set; }

        public MovieOriginOutput MovieOrigin { get; set; }

        public PagingOutput<MovieCommentOutput> MovieComments { get; set; }
        public int MovieCommentsCount { get; set; }

        public PagingOutput<QuestionTitleOutput> QuestionTitles { get; set; }
    }
}
