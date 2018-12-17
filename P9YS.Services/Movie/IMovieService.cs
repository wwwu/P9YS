using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.Services.Movie.Dto;

namespace P9YS.Services.Movie
{
    public interface IMovieService
    {
        Task<PagingOutput<MovieListOutput>> GetMoviesByConditionAsync(PagingInput<ConditionInput> pagingInput);

        Task<Dto.MovieInfoOutput> GetMovieInfoAsync(int movieId);

        Task<List<Dto.MovieSeriesOutput>> GetMovieSeriesAsync(int seriesId);

        Task<Dto.MovieOriginOutput> GetMovieOriginAsync(int movieId);
    }
}