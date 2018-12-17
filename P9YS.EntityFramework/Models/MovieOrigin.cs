﻿using P9YS.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P9YS.EntityFramework.Models
{
    public class MovieOrigin
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        [ForeignKey("MovieId")]
        public virtual Movie Movie { get; set; }

        public MovieOriginTypeEnum OriginType { get; set; }

        public decimal Score { get; set; }

        [StringLength(200)]
        public string Url { get; set; }
    }
}
