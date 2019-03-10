using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.Services.MovieResource.Dto;

namespace P9YS.Services.MovieResource
{
    public interface IMovieResourceService
    {
        Task<List<MovieOnlinePlayOutput>> GetMovieOnlinePlays(int movieId);
        Task<List<MovieResourceOutput>> GetMovieResources(int movieId);
        Task<PagingOutput<MovieResource_Manage_Output>> GetResources(PagingInput<MovieResource_Search_Input> pagingInput);
        Task<Result> AddResource(MovieResourceInput movieResourceInput);
        Task<Result> UpdResource(MovieResourceInput movieResourceInput);
        Task<Result> DelResource(int id);
    }
}