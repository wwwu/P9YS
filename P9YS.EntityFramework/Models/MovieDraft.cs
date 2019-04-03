using P9YS.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.EntityFramework.Models
{
    public class MovieDraft
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string DyUrl { get; set; }

        [Required]
        [StringLength(200)]
        public string DoubanUrl { get; set; }

        [Required]
        [StringLength(50)]
        public string MovieName { get; set; }

        public string ImgData { get; set; }

        public string Resoures { get; set; }

        public string DoubanHtml { get; set; }

        public MovieDraftStatusEnum Status { get; set; }

        public DateTime AddTime { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
