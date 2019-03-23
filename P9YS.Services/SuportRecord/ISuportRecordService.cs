using System;
using System.Threading.Tasks;
using P9YS.Services.SuportRecord.Dto;

namespace P9YS.Services.SuportRecord
{
    public interface ISuportRecordService
    {
        Task<bool> AddSuportRecord(SuportRecord_Input suportRecordInput);
        Task<int> UpdSuportsJob(DateTime endTime);
    }
}