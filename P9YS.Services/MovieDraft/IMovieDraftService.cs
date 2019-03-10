using System.Threading.Tasks;
using P9YS.Services.MovieDraft.Dto;

namespace P9YS.Services.MovieDraft
{
    public interface IMovieDraftService
    {
        Task<PagingOutput<MovieDraftOutput>> GetMovieDrafts(PagingInput<ConditionInput> pagingInput);

        Task<MovieDraftDetailOutput> GetMovieDraftDetail(int movieDraftId);
    }
}