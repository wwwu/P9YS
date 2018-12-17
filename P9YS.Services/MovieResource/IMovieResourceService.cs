using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.Services.MovieResource.Dto;

namespace P9YS.Services.MovieResource
{
    public interface IMovieResourceService
    {
        Task<List<MovieOnlinePlayOutput>> GetMovieOnlinePlays(int movieId);
        Task<List<MovieResourceOutput>> GetMovieResources(int movieId);
    }
}