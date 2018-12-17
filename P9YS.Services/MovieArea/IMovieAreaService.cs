using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.Services.MovieArea.Dto;

namespace P9YS.Services.MovieArea
{
    public interface IMovieAreaService
    {
        Task<List<MovieAreaOutput>> GetMovieAreasAsync();
    }
}