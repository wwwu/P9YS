using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.Services.MovieResource.Dto
{
    public class MovieResourceInput
    {
        public int? Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int Size { get; set; }

        [Required]
        [StringLength(50)]
        public string Resolution { get; set; }

        [Required]
        [StringLength(50)]
        public string Subtitle { get; set; }

        [Required]
        [StringLength(50)]
        public string Dub { get; set; }

        public int MovieId { get; set; }

        public int UserId { get; set; }
    }
}
