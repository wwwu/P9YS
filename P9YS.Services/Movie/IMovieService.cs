using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.Services.Movie.Dto;

namespace P9YS.Services.Movie
{
    public interface IMovieService
    {
        Task<PagingOutput<MovieListOutput>> GetMoviesByConditionAsync(PagingInput<ConditionInput> pagingInput);

        Task<MovieInfoOutput> GetMovieInfoAsync(int movieId);

        Task<List<MovieSeriesOutput>> GetMovieSeriesAsync(int seriesId);

        Task<MovieOriginOutput> GetMovieOriginAsync(int movieId);

        Task<PagingOutput<Movie_Manage_Output>> GetMoviesAsync(PagingInput<ConditionInput> pagingInput);

        Task<Movie_Manage_Output> GetMovieAsync(int movieId);

        Task<Result> UpdMovieAsync(Movie_Manage_Input input);

        Task<Result> DelMovieAsync(int movieId);
    }
}