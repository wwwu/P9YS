using System.Threading.Tasks;
using P9YS.Services.MovieDraft.Dto;

namespace P9YS.Services.MovieDraft
{
    public interface IMovieDraftService
    {
        Task<PagingOutput<MovieDraft_Output>> GetMovieDrafts(PagingInput<GetMovieDrafts_Condition_Input> pagingInput);

        Task<MovieDraft_Detail_Input> GetMovieDraftDetail(int movieDraftId);

        Task<Result> AddMovie(MovieDraft_Detail_Input movieDraftDetailInput);
    }
}