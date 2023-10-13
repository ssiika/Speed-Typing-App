using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Typing_App_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private static List<Record> mockRecords = new List<Record> {
            new Record(),
            new Record { Length = (Length)2, Time = 10 }
        };
        // GET: api/records
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Record>>>> GetUserRecords()
        {
            var serviceResponse = new ServiceResponse<List<Record>>();

            serviceResponse.Data = mockRecords;

            return Ok(serviceResponse);
        }
    }
}
