using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.RatingRecord.Dto
{
    public class GetRatings_Input
    {
        public int? MovieId { get; set; }

        public int? UserId { get; set; }

        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
