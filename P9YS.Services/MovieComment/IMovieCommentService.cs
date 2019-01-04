using System.Threading.Tasks;
using P9YS.Services.MovieComment.Dto;
using P9YS.Services.RatingRecord.Dto;

namespace P9YS.Services.MovieComment
{
    public interface IMovieCommentService
    {
        Task<PagingOutput<MovieCommentOutput>> GetCommentsAndReplyAsync(PagingInput<int> pagingInput);
        Task<int> GetCommentsCountByMovieAsync(int movieId);
        Task<bool> AddMovieCommentAsync(MovieCommentInput movieCommentInput);
        Task<PagingOutput<MovieComment_Manage_Output>> GetCommentsAsync(PagingInput<GetRatingsInput> pagingInput);
        Task<Result> DelCommentAsync(int id);
    }
}