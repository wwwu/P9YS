using System.Threading.Tasks;
using P9YS.Services.MovieComment.Dto;

namespace P9YS.Services.MovieComment
{
    public interface IMovieCommentService
    {
        Task<PagingOutput<MovieCommentOutput>> GetCommentsAndReplyAsync(PagingInput<int> pagingInput);
        Task<int> GetCommentsCountByMovieAsync(int movieId);
        Task<bool> AddMovieCommentAsync(Dto.MovieCommentInput movieCommentInput);
    }
}