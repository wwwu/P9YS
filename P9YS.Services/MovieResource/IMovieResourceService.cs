using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.Services.MovieResource.Dto;

namespace P9YS.Services.MovieResource
{
    public interface IMovieResourceService
    {
        Task<List<MovieOnlinePlay_Output>> GetMovieOnlinePlays(int movieId);
        Task<List<MovieResource_Output>> GetMovieResources(int movieId);
        Task<PagingOutput<MovieResource_Manage_Output>> GetResources(PagingInput<MovieResource_Search_Input> pagingInput);
        Task<Result> AddResource(MovieResource_Input movieResourceInput);
        Task<Result> UpdResource(MovieResource_Input movieResourceInput);
        Task<Result> DelResource(int id);
    }
}