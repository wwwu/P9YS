﻿using System.Threading.Tasks;
using P9YS.Services.SuportRecord.Dto;

namespace P9YS.Services.SuportRecord
{
    public interface ISuportRecordService
    {
        Task<bool> AddSuportRecord(SuportRecordInput suportRecordInput);
    }
}