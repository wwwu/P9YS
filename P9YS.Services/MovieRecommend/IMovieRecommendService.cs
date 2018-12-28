using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.Common.Enums;
using P9YS.EntityFramework.Models;
using P9YS.Services.MovieRecommend.Dto;

namespace P9YS.Services.MovieRecommend
{
    public interface IMovieRecommendService
    {
        Task<bool> AddRecommendAsync(MovieRecommendInput recommendInput);
        Task<bool> DelRecommendAsync(int id);
        Task<List<MovieRecommendOutput>> GetAnnualRecommendsAsync(int limit = 10);
        Task<List<MovieRecommendOutput>> GetRecentRecommendsAsync(int limit = 10);
        Task<List<RecommendOutput>> GetRecommendsAsync(RecommendTypeEnum recommendType);
    }
}