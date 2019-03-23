using System;
using System.Collections.Generic;
using System.Text;

namespace P9YS.Services.MovieQuestion.Dto
{
    public class Question_Output
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int AnswerCount { get; set; }

        public string UserAvatar { get; set; }

        public string UserNickName { get; set; }

        public int MovieId { get; set; }

        public DateTime AddTime { get; set; }

        public DateTime UpdTime { get; set; }
    }
}
