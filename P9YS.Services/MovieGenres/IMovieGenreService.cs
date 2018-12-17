using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.Services.MovieGenres.Dto;

namespace P9YS.Services.MovieGenres
{
    public interface IMovieGenreService
    {
        Task<List<MovieGenreOutput>> GetMovieGenresAsync();
    }
}