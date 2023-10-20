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
            new Record { Length = (Length)2, Time = 10 }
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

        // GET: api/records/5
        [HttpGet("{enumId}")]
        public async Task<ActionResult<ServiceResponse<List<GetRecordDto>>>> GetRecordsByLength(int enumId)
        {
            var response = await _recordService.GetRecordsByLength(enumId);
           
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
    }
}
