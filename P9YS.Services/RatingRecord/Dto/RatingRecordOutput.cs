using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.RatingRecord.Dto
{
    public class RatingRecordOutput
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public string MovieShortName { get; set; }

        public int UserId { get; set; }

        public string UserEmail { get; set; }

        public decimal Score { get; set; }

        public DateTime AddTime { get; set; }
    }
}
