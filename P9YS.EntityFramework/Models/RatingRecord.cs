using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P9YS.EntityFramework.Models
{
    public class RatingRecord
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int UserId { get; set; }

        [Range(0, 10)]

        public decimal Score { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public DateTime AddTime { get; set; }
    }
}
