using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.Services.MovieQuestion.Dto
{
    public class QuestionAnswer_Input
    {
        [Required]
        [StringLength(1000,MinimumLength =2)]
        public string Content { get; set; }
        public int MovieQuestionId { get; set; }
    }
}
