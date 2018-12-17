using System.Threading.Tasks;
using P9YS.Services.MovieQuestion.Dto;

namespace P9YS.Services.MovieQuestion
{
    public interface IMovieQuestionService
    {
        Task<bool> AddQuestionAsync(QuestionInput questionInput);
        Task<PagingOutput<QuestionTitleOutput>> GetQuestionTitlesAsync(PagingInput<int> pagingInput);
        Task<Dto.QuestionOutput> GetQuestionAsync(int questionId);
        Task<PagingOutput<Dto.QuestionAnswerOutput>> GetQuestionAnswersAsync(PagingInput<int> pagingInput);
        Task<bool> AddQuestionAnswerAsync(QuestionAnswerInput questionAnswerInput);
    }
}