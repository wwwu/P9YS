using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.EntityFramework.Models;
using P9YS.Services.MovieGenres.Dto;

namespace P9YS.Services.MovieGenres
{
    public interface IMovieGenreService
    {
        Task<Result> AddMovieGenreAsync(MoiveGenreInput moiveGenreInput);
        Task<List<MovieGenreOutput>> GetMovieGenresAsync();
        Task<PagingOutput<MovieGenre>> GetMovieGenresAsync(PagingInput<string> pagingInput);
        Task<Result> UpdMovieGenreAsync(MoiveGenreInput moiveGenreInput);
    }
}