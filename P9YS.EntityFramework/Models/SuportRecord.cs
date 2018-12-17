using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P9YS.EntityFramework.Models
{
    public class SuportRecord
    {
        public int Id { get; set; }

        public int MovieQuestionAnswerId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("MovieQuestionAnswerId")]
        public virtual MovieQuestionAnswer Movie { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public DateTime AddTime { get; set; }
    }
}
