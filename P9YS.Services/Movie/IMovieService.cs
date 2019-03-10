using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.Services.Movie.Dto;

namespace P9YS.Services.Movie
{
    public interface IMovieService
    {
        Task<PagingOutput<MovieListOutput>> GetMoviesByCondition(PagingInput<ConditionInput> pagingInput);

        Task<MovieInfoOutput> GetMovieInfo(int movieId);

        Task<List<MovieSeriesOutput>> GetMovieSeries(int seriesId);

        Task<MovieOriginOutput> GetMovieOrigin(int movieId);

        Task<PagingOutput<Movie_Manage_Output>> GetMovies(PagingInput<ConditionInput> pagingInput);

        Task<Movie_Manage_Output> GetMovie(int movieId);

        Task<Result> UpdMovie(Movie_Manage_Input input);

        Task<Result> DelMovie(int movieId);

        List<MovieDoubanOriginOutput> GetMoviesByOriginUpdTime(int count);

        Task UpdDoubanData(MovieDoubanOriginOutput movieDoubanOrigin);
    }
}