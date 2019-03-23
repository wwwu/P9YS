using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.Services.RatingRecord.Dto
{
    public class RatingRecord_Input
    {
        public int MovieId { get; set; }

        [Range(0, 10)]

        public decimal Score { get; set; }
    }
}
