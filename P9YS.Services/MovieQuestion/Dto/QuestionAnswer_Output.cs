using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.MovieQuestion.Dto
{
    public class QuestionAnswer_Output
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int Support { get; set; }

        public DateTime AddTime { get; set; }

        public DateTime UpdTime { get; set; }

        public int MovieQuestionId { get; set; }

        public string UserAvatar { get; set; }

        public string UserNickName { get; set; }
    }
}
