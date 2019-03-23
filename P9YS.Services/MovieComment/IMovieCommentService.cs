using System.Threading.Tasks;
using P9YS.Services.MovieComment.Dto;
using P9YS.Services.RatingRecord.Dto;

namespace P9YS.Services.MovieComment
{
    public interface IMovieCommentService
    {
        Task<PagingOutput<MovieComment_Output>> GetCommentsAndReply(PagingInput<int> pagingInput);
        Task<int> GetCommentsCountByMovie(int movieId);
        Task<bool> AddMovieComment(MovieComment_Input movieCommentInput);
        Task<PagingOutput<MovieComment_Manage_Output>> GetComments(PagingInput<GetRatings_Input> pagingInput);
        Task<Result> DelComment(int id);
    }
}