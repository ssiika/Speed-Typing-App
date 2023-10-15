using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Typing_App_API.Models;

namespace Typing_App_API.Services.RecordService
{
    public class RecordService : IRecordService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;
        

        public RecordService(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            DataContext context
            )
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;           
        }
        public async Task<ServiceResponse<List<GetRecordDto>>> GetUserRecords()
        {
            var serviceResponse = new ServiceResponse<List<GetRecordDto>>();

            try
            {          
                if (_httpContextAccessor.HttpContext == null)
                {
                    throw new Exception("User not found");
                }

                string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    throw new Exception("Could not find user id");
                }


                var userRecords = await _context.Records.Where(
                    record => record.User != null && record.User.Id == int.Parse(userId)
                    ).ToListAsync();
                serviceResponse.Data = userRecords.Select(record => _mapper.Map<GetRecordDto>(record)).ToList();

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
} 
