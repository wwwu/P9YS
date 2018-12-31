using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.EntityFramework.Models;
using P9YS.Services.MovieArea.Dto;

namespace P9YS.Services.MovieArea
{
    public interface IMovieAreaService
    {
        Task<Result> AddMovieAreaAsync(MovieAreaInput movieAreaInput);
        Task<List<MovieAreaOutput>> GetMovieAreasAsync();
        Task<PagingOutput<EntityFramework.Models.MovieArea>> GetMovieAreasAsync(PagingInput<string> pagingInput);
        Task<Result> UpdMovieAreaAsync(MovieAreaInput movieAreaInput);
    }
}