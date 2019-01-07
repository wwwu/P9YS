using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.MovieResource.Dto
{
    public class MovieResource_Manage_Output
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserNickName { get; set; }
        public int MovieId { get; set; }
        public string MovieShortName { get; set; }
        public string Content { get; set; }
        public string Size { get; set; }
        public string Resolution { get; set; }
        public string Subtitle { get; set; }
        public string Dub { get; set; }
        public DateTime UpdTime { get; set; }
        public DateTime AddTime { get; set; }
    }
}
