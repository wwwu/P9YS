using P9YS.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P9YS.Services.MovieDraft.Dto
{
    public class ConditionInput
    {
        [StringLength(50)]
        public string Keyword { get; set; }

        public MovieDraftStatusEnum? Status { get; set; }

        public DateTime? BeginDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
