using System.Collections.Generic;
using System.Threading.Tasks;
using P9YS.Common.Enums;
using P9YS.EntityFramework.Models;
using P9YS.Services.MovieRecommend.Dto;

namespace P9YS.Services.MovieRecommend
{
    public interface IMovieRecommendService
    {
        Task<bool> AddRecommend(MovieRecommendInput recommendInput);
        Task<bool> DelRecommend(int id);
        Task<List<MovieRecommendOutput>> GetAnnualRecommends(int limit = 10);
        Task<List<MovieRecommendOutput>> GetRecentRecommends(int limit = 10);
        Task<List<RecommendOutput>> GetRecommends(RecommendTypeEnum recommendType);
    }
}