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
        public MovieInfo_Output MovieInfo { get; set; }
        public Question_Output QuestionInfo { get; set; }
        public List<QuestionTitle_Output> QuestionTitles { get; set; }
        public PagingOutput<QuestionAnswer_Output> QuestionAnswers { get; set; }
    }
}
