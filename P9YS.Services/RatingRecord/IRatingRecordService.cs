using System;
using System.Threading.Tasks;
using P9YS.Services.RatingRecord.Dto;

namespace P9YS.Services.RatingRecord
{
    public interface IRatingRecordService
    {
        Task<bool> AddRatingRecord(RatingRecord_Input ratingRecordInput);
        Task<PagingOutput<RatingRecord_Output>> GetRatings(PagingInput<GetRatings_Input> pagingInput);
        Task<int> UpdRatingsJob(DateTime endTime);
        Task<int> OptimizeDatabase();
    }
}