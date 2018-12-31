using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.Services.MovieGenres.Dto
{
    public class MoiveGenreInput
    {
        public int Id { get; set; }

        [StringLength(50,MinimumLength = 1)]
        [Required]
        public string Name { get; set; }
    }
}
