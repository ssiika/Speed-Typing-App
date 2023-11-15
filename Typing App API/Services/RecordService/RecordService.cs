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


                var userRecords = await _context.Records
                    .Include("User")
                    .Where(record => record.User != null && record.User.Id == int.Parse(userId))
                    .Take(20)
                    .OrderByDescending(record => record.Time)
                    .ToListAsync();

                serviceResponse.Data = userRecords.Select(record => _mapper.Map<GetRecordDto>(record)).ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetRecordDto>> AddRecord(AddRecordDto newRecord)
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
                
                var user = await _context.Users.FindAsync(Int32.Parse(userId)) ?? 
                    throw new Exception("Could not find user");

                var newRecordWUser = new Record
                {
                    Date = DateTime.Today,
                    Time = newRecord.Time,
                    User = user,
                };
            
                _context.Records.Add(newRecordWUser);
                await _context.SaveChangesAsync();

                var addedRecord = await _context.Records.FindAsync(newRecordWUser.Id) ?? 
                    throw new Exception("User not added to database successfully");

                serviceResponse.Data = _mapper.Map<GetRecordDto>(addedRecord);

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }       

        public async Task<ServiceResponse<List<GetRecordDto>>> DeleteRecord(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetRecordDto>>();


            try
            {
                if (_httpContextAccessor.HttpContext == null)
                {
                    throw new Exception("User not found");
                }

                string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                    throw new Exception("Could not find user id");

                var record = await _context.Records.Include("User").FirstOrDefaultAsync(record => record.Id == id);

                if (record is null)
                {
                    throw new Exception($"Record with id {id} not found");
                }

                if (record.User?.Id != Int32.Parse(userId))
                {
                    throw new Exception("User does not have permission to edit this record");
                }

                _context.Records.Remove(record);
                await _context.SaveChangesAsync();

                var updatedList = await _context.Records.ToListAsync();
                serviceResponse.Data = updatedList.Select(record => _mapper.Map<GetRecordDto>(record)).ToList();

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
