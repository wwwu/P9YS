using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.EntityFramework.Models;
using P9YS.Services.MovieArea.Dto;

namespace P9YS.Services.MovieArea
{
    public interface IMovieAreaService
    {
        Task<Result> AddMovieArea(MovieAreaInput movieAreaInput);
        Task<List<MovieAreaOutput>> GetMovieAreas();
        Task<PagingOutput<EntityFramework.Models.MovieArea>> GetMovieAreas(PagingInput<string> pagingInput);
        Task<Result> UpdMovieArea(MovieAreaInput movieAreaInput);
        Task<int> GetMovieAreaId(string areaName);
    }
}