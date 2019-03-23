using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.Movie.Dto
{
    public class MovieOrigin_Douban_Output
    {
        public int MovieId { get; set; }

        public string FullName { get; set; }

        /// <summary>
        /// "豆瓣"Url
        /// </summary>
        public string Url { get; set; }

        public DateTime? UpdTime { get; set; }
    }
}
