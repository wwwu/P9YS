using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.MovieComment.Dto
{
    public class MovieComment_Manage_Output
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public string MovieShortName { get; set; }

        public int UserId { get; set; }

        public string UserEmail { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }

        public DateTime AddTime { get; set; }

        public DateTime UpdTime { get; set; }
    }
}
