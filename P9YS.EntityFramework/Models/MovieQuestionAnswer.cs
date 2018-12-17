using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P9YS.EntityFramework.Models
{
    public class MovieQuestionAnswer
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public int Support { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime UpdTime { get; set; }

        public int MovieQuestionId { get; set; }
        [ForeignKey("MovieQuestionId")]
        public virtual MovieQuestion MovieQuestion { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
