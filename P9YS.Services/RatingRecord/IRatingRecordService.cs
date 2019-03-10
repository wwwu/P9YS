using System;
using System.Threading.Tasks;
using P9YS.Services.RatingRecord.Dto;

namespace P9YS.Services.RatingRecord
{
    public interface IRatingRecordService
    {
        Task<bool> AddRatingRecord(RatingRecordInput ratingRecordInput);
        Task<PagingOutput<RatingRecordOutput>> GetRatings(PagingInput<GetRatingsInput> pagingInput);
        void UpdRatingsJob(DateTime endTime);
    }
}