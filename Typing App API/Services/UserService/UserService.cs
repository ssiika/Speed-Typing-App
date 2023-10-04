namespace Typing_App_API.Services.UserService
{
    public class UserService : IUserService
    {

        private static List<User> testUsers = new List<User> {
            new User(),
            new User {Id=1, Username="Testo"}
        };

        private readonly IMapper _mapper;
        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            var user = _mapper.Map<User>(newUser);
            user.Id = testUsers.Max(x => x.Id) + 1;
            testUsers.Add(user);
            serviceResponse.Data = testUsers.Select(user => _mapper.Map<GetUserDto>(user)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            serviceResponse.Data = testUsers.Select(user => _mapper.Map<GetUserDto>(user)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> GetSingle(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var user = testUsers.FirstOrDefault(user => user.Id == id);
            serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            return serviceResponse;
        }
    }
}
