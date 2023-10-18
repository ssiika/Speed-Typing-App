namespace Typing_App_API.Services.RecordService
{
    public interface IRecordService
    {
        Task<ServiceResponse<List<GetRecordDto>>> GetUserRecords();

        Task<ServiceResponse<GetRecordDto>> AddRecord();
    }
}
