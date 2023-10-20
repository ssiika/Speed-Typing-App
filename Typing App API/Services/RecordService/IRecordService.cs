﻿namespace Typing_App_API.Services.RecordService
{
    public interface IRecordService
    {
        Task<ServiceResponse<List<GetRecordDto>>> GetUserRecords();
        Task<ServiceResponse<List<GetRecordDto>>> GetRecordsByLength(int enumId);
        Task<ServiceResponse<GetRecordDto>> AddRecord(AddRecordDto newRecord);
        Task<ServiceResponse<List<GetRecordDto>>> DeleteRecord(int id);
    }
}
