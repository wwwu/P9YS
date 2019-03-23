using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.Services.MovieQuestion.Dto
{
    public class Question_Input
    {
        public int MovieId { get; set; }

        [Required]
        [StringLength(200,MinimumLength =2)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
