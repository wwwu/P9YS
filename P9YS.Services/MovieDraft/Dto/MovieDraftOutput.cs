using P9YS.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.MovieDraft.Dto
{
    public class MovieDraftOutput
    {
        public int Id { get; set; }

        public string DyUrl { get; set; }

        public string DoubanUrl { get; set; }

        public string MovieName { get; set; }

        public string Resoures { get; set; }

        public int Size { get; set; }

        public MovieDraftStatusEnum Status { get; set; }

        public DateTime AddTime { get; set; }
    }
}
