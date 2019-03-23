using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.Services.Movie.Dto;

namespace P9YS.Services.Movie
{
    public interface IMovieService
    {
        Task<PagingOutput<MovieList_Output>> GetMoviesByCondition(PagingInput<GetMovies_Condition_Input> pagingInput);

        Task<MovieInfo_Output> GetMovieInfo(int movieId);

        Task<List<MovieSeries_Output>> GetMovieSeries(int seriesId);

        Task<MovieOrigin_Output> GetMovieOrigin(int movieId);

        Task<PagingOutput<Movie_Manage_Output>> GetMovies(PagingInput<GetMovies_Condition_Input> pagingInput);

        Task<Movie_Manage_Output> GetMovie(int movieId);

        Task<Result> UpdMovie(Movie_Manage_Input input);

        Task<Result> DelMovie(int movieId);

        Task<List<MovieOrigin_Douban_Output>> GetMoviesByOriginUpdTime(int count);

        Task UpdDoubanData(MovieOrigin_Douban_Output movieDoubanOrigin);
    }
}