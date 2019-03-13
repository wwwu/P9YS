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
        /// <summary>
        /// 资源大小(mb)
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 分辨率
        /// </summary>
        public string Resolution { get; set; }
        /// <summary>
        /// 字幕
        /// </summary>
        public string Subtitle { get; set; }
        /// <summary>
        /// 配音
        /// </summary>
        public string Dub { get; set; }
        public DateTime UpdTime { get; set; }
        public DateTime AddTime { get; set; }
    }
}
