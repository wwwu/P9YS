using System.Threading.Tasks;
using P9YS.Services.MovieDraft.Dto;

namespace P9YS.Services.MovieDraft
{
    public interface IMovieDraftService
    {
        Task<PagingOutput<MovieDraftOutput>> GetMovieDrafts(PagingInput<ConditionInput> pagingInput);

        Task<MovieDraftDetailInput> GetMovieDraftDetail(int movieDraftId);

        Task<Result> AddMovie(MovieDraftDetailInput movieDraftDetailInput);
    }
}