using System.Threading.Tasks;
using P9YS.Services.MovieQuestion.Dto;

namespace P9YS.Services.MovieQuestion
{
    public interface IMovieQuestionService
    {
        Task<bool> AddQuestion(QuestionInput questionInput);
        Task<PagingOutput<QuestionTitleOutput>> GetQuestionTitles(PagingInput<int> pagingInput);
        Task<QuestionOutput> GetQuestion(int questionId);
        Task<PagingOutput<QuestionAnswerOutput>> GetQuestionAnswers(PagingInput<int> pagingInput);
        Task<bool> AddQuestionAnswer(QuestionAnswerInput questionAnswerInput);
    }
}