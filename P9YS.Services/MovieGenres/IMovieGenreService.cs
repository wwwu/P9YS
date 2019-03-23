using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.EntityFramework.Models;
using P9YS.Services.MovieGenres.Dto;

namespace P9YS.Services.MovieGenres
{
    public interface IMovieGenreService
    {
        Task<Result> AddMovieGenre(MoiveGenre_Input moiveGenreInput);
        Task<List<MovieGenre_Output>> GetMovieGenres();
        Task<PagingOutput<MovieGenre>> GetMovieGenres(PagingInput<string> pagingInput);
        Task<Result> UpdMovieGenre(MoiveGenre_Input moiveGenreInput);
    }
}