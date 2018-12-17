using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P9YS.EntityFramework.Models
{
    public class MovieType
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int MovieGenreId { get; set; }
        [ForeignKey("MovieGenreId")]
        public virtual MovieGenre MovieGenre { get; set; }
    }
}
