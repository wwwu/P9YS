using P9YS.Common.Enums;
using P9YS.Services.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.Services.Movie.Dto
{
    public class GetMovies_Condition_Input
    {
        [StringLength(50)]
        public string Keyword { get; set; }

        public int? MovieAreaId { get; set; }

        public int? MovieTypeId { get; set; }

        public MovieListSortEnum Sort { get; set; } = MovieListSortEnum.LastModify;

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
