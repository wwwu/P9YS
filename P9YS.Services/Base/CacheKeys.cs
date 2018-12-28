using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services
{
    public static class CacheKeys
    {
        /// <summary>
        /// 默认缓存30分钟
        /// </summary>
        public const int DefaultMinutes = 30;

        /// <summary>
        /// 首页轮播
        /// </summary>
        public const string Carousels = "Carousels";

        /// <summary>
        /// 年度推荐
        /// </summary>
        public const string AnnualRecommends = "AnnualRecommends";

        /// <summary>
        /// 近期推荐
        /// </summary>
        public const string RecentRecommends = "RecentRecommends";

        /// <summary>
        /// 地区
        /// </summary>
        public const string MovieAreas = "MovieAreas";

        /// <summary>
        /// 影片类型
        /// </summary>
        public const string MovieGenres = "MovieGenres";
    }
}
