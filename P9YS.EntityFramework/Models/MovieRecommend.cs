using P9YS.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P9YS.EntityFramework.Models
{
    public class MovieRecommend
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public RecommendTypeEnum Type { get; set; }

        public int Sort { get; set; }

        public DateTime AddTime { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }
    }
}
