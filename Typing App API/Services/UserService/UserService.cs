namespace Typing_App_API.Services.UserService
{
    public class UserService : IUserService
    {

        private static List<User> testUsers = new List<User> {
            new User(),
            new User {Id=1, Username="Testo"}
        };

        public async Task<ServiceResponse<List<User>>> AddUser(User newUser)
        {
            var serviceResponse = new ServiceResponse<List<User>>();
            testUsers.Add(newUser);
            serviceResponse.Data = testUsers;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<User>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<User>>();
            serviceResponse.Data = testUsers;
            return serviceResponse;
        }

        public async Task<ServiceResponse<User>> GetSingle(int id)
        {
            var serviceResponse = new ServiceResponse<User>();
            var user = testUsers.FirstOrDefault(user => user.Id == id);
            serviceResponse.Data = user;
            return serviceResponse;
        }
    }
}
