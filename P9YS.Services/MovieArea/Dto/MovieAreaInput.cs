using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.Services.MovieArea.Dto
{
    public class MovieAreaInput
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength =1)]
        public string Area { get; set; }

        public int Other { get; set; }
    }
}
