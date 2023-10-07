﻿namespace Typing_App_API.Services.UserService
{
    public class UserService : IUserService
    {

        private static List<User> testUsers = new List<User> {
            new User(),
            new User {Id=1, Username="Testo"}
        };

        private readonly IMapper _mapper;

        private readonly DataContext _context;

        public UserService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            var newMappedUser = _mapper.Map<User>(newUser);
            newMappedUser.Id = testUsers.Max(user => user.Id) + 1;
            testUsers.Add(newMappedUser);
            serviceResponse.Data = testUsers.Select(user => _mapper.Map<GetUserDto>(user)).ToList();
            return serviceResponse;
        } 

        public async Task<ServiceResponse<List<GetUserDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            var dbUsers = await _context.Users.ToListAsync();
            serviceResponse.Data = dbUsers.Select(user => _mapper.Map<GetUserDto>(user)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> GetSingle(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var user = testUsers.FirstOrDefault(user => user.Id == id);
            serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(int id, UpdateUserDto updatedUser)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();

            try
            {
                var user = testUsers.FirstOrDefault(user => user.Id == id);

                if (user is null)
                {
                    throw new Exception($"User with id {id} not found");
                }

                user.Username = updatedUser.Username;
                user.Password = updatedUser.Password;

                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();

            try
            {
                var user = testUsers.FirstOrDefault(user => user.Id == id);

                if (user is null)
                {
                    throw new Exception($"User with id {id} not found");
                }

                testUsers.Remove(user);

                serviceResponse.Data = testUsers.Select(user => _mapper.Map<GetUserDto>(user)).ToList();
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
