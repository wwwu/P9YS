using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.Services.MovieComment.Dto
{
    public class MovieComment_Input
    {
        public int MovieId { get; set; }

        public int ParentId { get; set; }

        [Required]
        [StringLength(1000,MinimumLength =1)]
        public string Content { get; set; }
    }
}
