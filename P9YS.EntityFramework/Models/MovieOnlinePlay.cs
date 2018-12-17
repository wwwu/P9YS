using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P9YS.EntityFramework.Models
{
    public class MovieOnlinePlay
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string WebSiteName { get; set; }

        [StringLength(50)]
        public string Price { get; set; }

        public string Url { get; set; }

        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
    }
}
