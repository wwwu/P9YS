using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.MovieResource.Dto
{
    public class MovieResource_Search_Input
    {
        public int? UserId { get; set; }

        public int? MovieId { get; set; }

        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}
