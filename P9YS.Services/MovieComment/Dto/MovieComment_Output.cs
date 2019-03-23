using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.MovieComment.Dto
{
    public class MovieComment_Output
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string UserAvatar { get; set; }
        public string UserNickName { get; set; }
        public string Content { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime UpdTime { get; set; }

        public virtual List<MovieComment_Output> Replys { get; set; }
    }
}
