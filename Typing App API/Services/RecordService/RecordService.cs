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

        public async Task<ServiceResponse<GetRecordDto>> AddRecord()
        {
            var serviceResponse = new ServiceResponse<GetRecordDto>();
         

            try
            {
                if (_httpContextAccessor.HttpContext == null)
                {
                    throw new Exception("User not found");
                }

                string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? 
                    throw new Exception("Could not find user id");

                var user = await _context.Users.FindAsync(userId) ?? 
                    throw new Exception("Could not find user");

                var newRecord = new AddRecordDto
                {
                    Length = (Length)1,
                    Time = 10,
                    User = user,
                };

                var newMappedRecord = _mapper.Map<Record>(newRecord);
                _context.Records.Add(newMappedRecord);
                await _context.SaveChangesAsync();

                var addedRecord = await _context.Records.FindAsync(newMappedRecord.Id) ?? 
                    throw new Exception("User not added to database successfully");

                serviceResponse.Data = _mapper.Map<GetRecordDto>(addedRecord);

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
