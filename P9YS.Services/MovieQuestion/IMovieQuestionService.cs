using System.Threading.Tasks;
using P9YS.Services.MovieQuestion.Dto;

namespace P9YS.Services.MovieQuestion
{
    public interface IMovieQuestionService
    {
        Task<bool> AddQuestion(Question_Input questionInput);
        Task<PagingOutput<QuestionTitle_Output>> GetQuestionTitles(PagingInput<int> pagingInput);
        Task<Question_Output> GetQuestion(int questionId);
        Task<PagingOutput<QuestionAnswer_Output>> GetQuestionAnswers(PagingInput<int> pagingInput);
        Task<bool> AddQuestionAnswer(QuestionAnswer_Input questionAnswerInput);
    }
}