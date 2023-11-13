using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Typing_App_API.Services.RecordService;

namespace Typing_App_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private static List<Record> mockRecords = new List<Record> {
            new Record(),
            new Record { Date = DateTime.Now, Time = 10 }
        };

        private readonly IRecordService _recordService;

        public RecordsController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        // GET: api/records
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetRecordDto>>>> GetUserRecords()
        {
            var response = await _recordService.GetUserRecords();

            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // POST: api/records
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetRecordDto>>>> AddRecord(AddRecordDto newRecord)
        {
            var response = await _recordService.AddRecord(newRecord);

            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        // DELETE api/records/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetRecordDto>>>> DeleteRecord(int id)
        {
            var response = await _recordService.DeleteRecord(id);

            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
