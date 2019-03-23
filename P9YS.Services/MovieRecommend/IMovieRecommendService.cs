using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.Common.Enums;
using P9YS.EntityFramework.Models;
using P9YS.Services.MovieRecommend.Dto;

namespace P9YS.Services.MovieRecommend
{
    public interface IMovieRecommendService
    {
        Task<bool> AddRecommend(MovieRecommend_Input recommendInput);
        Task<bool> DelRecommend(int id);
        Task<List<MovieRecommend_Output>> GetAnnualRecommends(int limit = 10);
        Task<List<MovieRecommend_Output>> GetRecentRecommends(int limit = 10);
        Task<List<Recommend_Output>> GetRecommends(RecommendTypeEnum recommendType);
    }
}