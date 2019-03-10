using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.EntityFramework.Models;
using P9YS.Services.MovieGenres.Dto;

namespace P9YS.Services.MovieGenres
{
    public interface IMovieGenreService
    {
        Task<Result> AddMovieGenre(MoiveGenreInput moiveGenreInput);
        Task<List<MovieGenreOutput>> GetMovieGenres();
        Task<PagingOutput<MovieGenre>> GetMovieGenres(PagingInput<string> pagingInput);
        Task<Result> UpdMovieGenre(MoiveGenreInput moiveGenreInput);
    }
}