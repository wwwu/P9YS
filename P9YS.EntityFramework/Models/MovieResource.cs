using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P9YS.EntityFramework.Models
{
    public class MovieResource
    {
        public int Id { get; set; }

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

        public DateTime AddTime { get; set; }

        public DateTime UpdTime { get; set; }

        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
