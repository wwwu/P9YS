using P9YS.Services;
using P9YS.Services.Movie.Dto;
using P9YS.Services.MovieQuestion.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P9YS.Web.Models
{
    public class MovieQuestionViewModel
    {
        public MovieInfoOutput MovieInfo { get; set; }
        public QuestionOutput QuestionInfo { get; set; }
        public List<QuestionTitleOutput> QuestionTitles { get; set; }
        public PagingOutput<QuestionAnswerOutput> QuestionAnswers { get; set; }
    }
}
